<template>
  <m-popup @close="$emit('close')">
    <template #title>
      <h2>Thông tin nhân viên</h2>
      <m-check-box style="margin-left: 40px">Là khách hàng</m-check-box>
      <m-check-box style="margin-left: 18px">Là nhà cung cấp</m-check-box>
    </template>
    <m-row :gx="26">
      <m-col :span="6">
        <m-row :gx="6" :gy="12">
          <m-col :span="5">
            <m-field required label="Mã">
              <m-input
                v-model="employee.EmployeeCode"
                :error="errorMessages.EmployeeCode"
                @touch="handleFieldTouch('EmployeeCode')"
                autoFocus
              />
            </m-field>
          </m-col>
          <m-col :span="7"
            ><m-field required label="Tên">
              <m-input
                v-model="employee.EmployeeName"
                :error="errorMessages.EmployeeName"
                @touch="handleFieldTouch('EmployeeName')"
              /> </m-field
          ></m-col>
          <m-col :span="12">
            <m-field required label="Đơn vị">
              <m-combobox
                style="width: 100%"
                :items="departments"
                :loading="departmentLoading"
                v-model="employee.DepartmentName"
                tableMenu
                :headers="departmentHeaders"
                :filterFn="departmentFilter"
                @select="handleDepartmentSelect"
                @open="loadDepartments"
                labelMember="DepartmentName"
                :error="errorMessages.DepartmentId"
                @touch="handleFieldTouch('DepartmentId')"
              />
            </m-field>
          </m-col>
          <m-col :span="12">
            <m-field label="Chức danh">
              <m-input v-model="employee.EmployeePosition" />
            </m-field>
          </m-col>
        </m-row>
      </m-col>
      <m-col :span="6">
        <m-row :gx="6" :gy="12">
          <m-col :span="5">
            <m-field label="Ngày sinh">
              <m-input type="date" v-model="employee.DateOfBirth" />
            </m-field>
          </m-col>
          <m-col :span="7"
            ><m-field label="Giới tính" style="padding-left: 10px">
              <div class="flex items-center" style="padding-top: 5px">
                <m-radio value="1" v-model="employee.Gender">Nam</m-radio>
                <m-radio value="0" v-model="employee.Gender">Nữ</m-radio>
                <m-radio value="2" v-model="employee.Gender">Khác</m-radio>
              </div>
            </m-field></m-col
          >
          <m-col :span="7">
            <m-field label="Số CMND">
              <m-input v-model="employee.IdentityNumber" />
            </m-field>
          </m-col>
          <m-col :span="5">
            <m-field label="Ngày cấp">
              <m-input type="date" v-model="employee.IdentityDate" />
            </m-field>
          </m-col>
          <m-col :span="12">
            <m-field label="Nơi cấp">
              <m-input v-model="employee.IdentityPlace" />
            </m-field>
          </m-col>
        </m-row>
      </m-col>
    </m-row>
    <m-row style="margin-top: 56px" :gx="6" :gy="12">
      <m-col :span="12">
        <m-field label="Địa chỉ">
          <m-input v-model="employee.Address" />
        </m-field>
      </m-col>
      <m-col :span="3">
        <m-field label="ĐT di động">
          <m-input v-model="employee.PhoneNumber" />
        </m-field>
      </m-col>
      <m-col :span="3">
        <m-field label="ĐT cố định">
          <m-input v-model="employee.TelephoneNumber" />
        </m-field>
      </m-col>
      <m-col :span="3">
        <m-field label="Email">
          <m-input v-model="employee.Email" :error="errorMessages.Email" />
        </m-field>
      </m-col>
      <m-col :span="3" />
      <m-col :span="3">
        <m-field label="Số tài khoản">
          <m-input v-model="employee.BankAccountNumber" />
        </m-field>
      </m-col>
      <m-col :span="3">
        <m-field label="Tên ngân hàng">
          <m-input v-model="employee.BankName" />
        </m-field>
      </m-col>
      <m-col :span="3">
        <m-field label="Chi nhánh">
          <m-input v-model="employee.BankBranchName" />
        </m-field>
      </m-col>
    </m-row>
    <template #footer>
      <div>
        <m-button color="secondary" @click="$emit('close')">Hủy</m-button>
      </div>
      <div>
        <m-button color="secondary" @click="handleSave">Cất</m-button>
        &nbsp;
        <m-button @click="handleSaveAndAdd">Cất và thêm</m-button>
      </div>
    </template>
  </m-popup>
  <m-spinner v-if="popupLoading" />
  <m-message-dialog
    info
    :text="commonError"
    v-if="showErrorMessage"
    icon="error"
    @close="handleCloseErrorDialog"
  />
</template>
<script>
import MPopup from "../components/bases/MPopup.vue";
import MButton from "../components/bases/MButton.vue";
import MCheckBox from "../components/bases/MCheckBox.vue";
import MRow from "../components/bases/Grid/MRow.vue";
import MCol from "../components/bases/Grid/MCol.vue";
import MField from "../components/bases/MField.vue";
import MInput from "../components/bases/MInput.vue";
import MCombobox from "../components/bases/MCombobox.vue";
import MRadio from "../components/bases/MRadio.vue";
import MSpinner from "../components/bases/MSpinner.vue";
import MMessageDialog from "../components/bases/MMessageDialog.vue";
export default {
  components: {
    MPopup,
    MButton,
    MCheckBox,
    MRow,
    MCol,
    MField,
    MInput,
    MCombobox,
    MRadio,
    MSpinner,
    MMessageDialog,
  },
  emits: ["close", "saveAndAdd"],
  props: {
    employeeId: String, //id cua employee duoc chon de sua
  },
  data() {
    return {
      employee: this.getInitialEmployee(),
      departments: undefined, //danh sach don vi
      departmentHeaders: [
        { text: "Mã đơn vị", value: "DepartmentId" },
        { text: "Tên đơn vị", value: "DepartmentName" },
      ], //header cua table don vi trong combobox
      departmentLoading: false, //combobox loading
      popupLoading: false, //co hien spinner ko
      errors: {
        //loi cua cac truong
        EmployeeCode: {
          message: "", //thong tin loi
          touched: false, //da duoc chinh sua lan dau tien
        },
        EmployeeName: {
          message: "",
          touched: false,
        },
        DepartmentId: {
          message: "",
          touched: false,
        },
        Email: {
          message: "",
          touched: false,
        },
      },
      showErrorMessage: false, //co hien thong bao loi ko
      commonError: undefined, //loi chung
    };
  },
  created() {
    if (this.employeeId) {
      //neu la form sua thi load employee
      this.loadEmployee();
    } else {
      //la form them thi lay employee code moi
      this.getNewEmployeeCode();
    }
  },
  methods: {
    //tra ve employee khoi tao cho form them moi
    getInitialEmployee() {
      return {
        //employee khoi tao
        EmployeeCode: "",
        EmployeeName: "",
        DepartmentId: "",
        Gender: 1,
        DateOfBirth: "",
        PhoneNumber: "",
        Email: "",
        Address: "",
        IdentityNumber: "",
        IdentityPlace: "",
        TelephoneNumber: "",
        EmployeePosition: "",
        DepartmentName: "",
        BankName: "",
        BankAccountNumber: "",
        BankBranchName: "",
      };
    },
    //lay ma nv moi
    async getNewEmployeeCode() {
      try {
        const { data: code } = await this.$axios.get(
          "http://amis.manhnv.net/api/v1/Employees/NewEmployeeCode"
        );
        this.employee.EmployeeCode = code;
      } catch (err) {
        this.handleAxiosError(err);
      }
    },
    handleFieldTouch(fieldName) {
      //field duoc sua lan dau tien
      this.errors[fieldName].touched = true;
      this.validate();
    },
    departmentFilter(department, search) {
      //ham loc du lieu cua combobox don vi
      const reg = new RegExp(search, "i");
      return reg.test(department.DepartmentName);
    },
    loadDepartments() {
      //ham load du lieu combobox don vi
      if (this.departments || this.departmentLoading) return; //neu da co du lieu or dang loading
      this.departmentLoading = true;
      this.$axios
        .get("http://amis.manhnv.net/api/v1/Departments")
        .then(({ data }) => {
          this.departments = data;
        })
        .finally(() => {
          this.departmentLoading = false;
        });
    },
    //ham xu ly chon don vi
    handleDepartmentSelect(department) {
      this.employee.DepartmentId = department?.DepartmentId;
    },
    //ham load nhan vien (form sua)
    async loadEmployee() {
      this.popupLoading = true;
      try {
        const { data: emp } = await this.$axios.get(
          `http://amis.manhnv.net/api/v1/Employees/${this.employeeId}`
        );
        if (emp.DepartmentId) {
          //load them ten don vi
          const { data: department } = await this.$axios.get(
            `http://amis.manhnv.net/api/v1/Departments/${emp.DepartmentId}`
          );
          emp.DepartmentName = department.DepartmentName;
        }
        //format lai ngay thang
        emp.DateOfBirth = this.$formatters.formatDate(emp.DateOfBirth, 2, "-");
        emp.IdentityDate = this.$formatters.formatDate(
          emp.IdentityDate,
          2,
          "-"
        );
        this.employee = emp;
      } catch (err) {
        this.handleAxiosError(err);
      } finally {
        this.popupLoading = false;
      }
    },
    /**
     * ham validate form
     * @returns {Boolean}
     */
    validate() {
      //validate EmployeeCode
      this.errors.EmployeeCode.message = this.employee.EmployeeCode
        ? undefined
        : "Không được để trống mã nhân viên!";
      //validate EmployeeName
      this.errors.EmployeeName.message = this.employee.EmployeeName
        ? undefined
        : "Không được để trống tên nhân viên!";
      //validate DepartmentId
      if (!this.employee.DepartmentId) {
        if (this.employee.DepartmentName) {
          this.errors.DepartmentId.message = "Đơn vị không có trong danh sách!";
        } else {
          this.errors.DepartmentId.message = "Không được để trống đơn vị!";
        }
      } else {
        this.errors.DepartmentId.message = undefined;
      }
      //validate Email
      if (this.employee.Email && !/^.+@.+\..+$/.test(this.employee.Email)) {
        this.errors.Email.message = "Email không hợp lệ!";
      } else {
        this.errors.Email.message = undefined;
      }
      return Object.keys(this.errors).every((k) => !this.errors[k].message); //kiem tra xem co loi nao ko
    },
    //ham lay loi dau tien
    getFirstError() {
      const fk = Object.keys(this.errorMessages).find(
        (k) => this.errorMessages[k]
      );
      return this.errorMessages[fk];
    },
    //ham luu thong tin nv
    handleSave() {
      Object.keys(this.errors).forEach((k) => {
        this.errors[k].touched = true;
      }); //touch tat ca truong de validate
      if (!this.validate()) {
        this.commonError = this.getFirstError(); //hien loi dau tien len message dialog
        this.showErrorMessage = true;
        return;
      }
      if (this.employeeId) {
        //neu la form sua
        this.updateEmployee();
      } else {
        //form them
        this.addEmployee();
      }
    },
    //bat su kien click nut cat va them
    handleSaveAndAdd() {
      this.$emit("saveAndAdd");
      //xoa du lieu tren form
      Object.keys(this.employee).forEach((k) => {
        this.employee[k] = "";
      });
      //reset error
      Object.keys(this.errors).forEach((k) => {
        this.errors[k].message = "";
        this.errors[k].touched = false;
      });
      this.commonError = "";
      this.employee = this.getInitialEmployee();
      this.getNewEmployeeCode();
    },
    //ham them nv
    async addEmployee() {
      this.popupLoading = true;
      try {
        await this.$axios.post(
          "http://amis.manhnv.net/api/v1/Employees",
          this.employee
        );
        this.$emit("close", 1); //dong form voi status=1
      } catch (err) {
        this.handleAxiosError(err);
      } finally {
        this.popupLoading = false;
      }
    },
    //ham cap nhat nv
    async updateEmployee() {
      this.popupLoading = true;
      try {
        await this.$axios.put(
          `http://amis.manhnv.net/api/v1/Employees/${this.employeeId}`,
          this.employee
        );
        this.$emit("close", 1); //dong form voi status=1
      } catch (err) {
        this.handleAxiosError(err);
      } finally {
        this.popupLoading = false;
      }
    },
    //ham xu ly loi tu axios
    handleAxiosError(err) {
      console.log(err.response);
      const userMsg = err.response?.data?.userMsg;
      if (userMsg) {
        this.commonError = userMsg;
      } else {
        this.commonError = "Có lỗi xảy ra!";
      }
      this.showErrorMessage = true;
    },
    handleCloseErrorDialog() {
      this.showErrorMessage = false;
      this.commonError = undefined;
    },
  },
  computed: {
    //tra ve loi cua cac truong
    errorMessages() {
      return Object.keys(this.errors).reduce((msgs, fieldName) => {
        const err = this.errors[fieldName];
        if (err.touched && err.message) {
          //neu da duoc touch va co loi
          msgs[fieldName] = err.message;
        }
        return msgs;
      }, {});
    },
  },
  watch: {
    employee: {
      //employee thay doi thi validate du lieu
      deep: true,
      handler() {
        this.validate();
      },
    },
  },
};
</script>
