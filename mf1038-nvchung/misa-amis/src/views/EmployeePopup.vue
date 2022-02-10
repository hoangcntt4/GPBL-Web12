<template>
  <m-popup @close="closeForm">
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
                name="EmployeeCode"
                :error="getInputError('EmployeeCode')"
                @blur="handleBlur"
                @input="handleInput"
                autoFocus
                maxlength="25"
              />
            </m-field>
          </m-col>
          <m-col :span="7"
            ><m-field required label="Tên">
              <m-input
                v-model="employee.FullName"
                name="FullName"
                :error="getInputError('FullName')"
                @blur="handleBlur"
                @input="handleInput"
                maxlength="200"
              /> </m-field
          ></m-col>
          <m-col :span="12">
            <m-field required label="Đơn vị">
              <m-combobox
                name="DepartmentId"
                @blur="handleBlur"
                @input="handleInput"
                :error="getInputError('DepartmentId')"
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
              />
            </m-field>
          </m-col>
          <m-col :span="12">
            <m-field label="Chức danh">
              <m-input
                v-model="employee.EmployeePosition"
                @input="handleInput"
                maxlength="128"
              />
            </m-field>
          </m-col>
        </m-row>
      </m-col>
      <m-col :span="6">
        <m-row :gx="6" :gy="12">
          <m-col :span="5">
            <m-field label="Ngày sinh">
              <m-input
                type="date"
                v-model="employee.DateOfBirth"
                name="DateOfBirth"
                :error="getInputError('DateOfBirth')"
                @blur="handleBlur"
                @input="handleInput"
              />
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
              <m-input
                v-model="employee.IdentityNumber"
                name="IdentityNumber"
                :error="getInputError('IdentityNumber')"
                @blur="handleBlur"
                @input="handleInput"
                maxlength="20"
              />
            </m-field>
          </m-col>
          <m-col :span="5">
            <m-field label="Ngày cấp">
              <m-input
                type="date"
                v-model="employee.IdentityDate"
                name="IdentityDate"
                :error="getInputError('IdentityDate')"
                @blur="handleBlur"
                @input="handleInput"
              />
            </m-field>
          </m-col>
          <m-col :span="12">
            <m-field label="Nơi cấp">
              <m-input
                v-model="employee.IdentityPlace"
                @input="handleInput"
                maxlength="255"
              />
            </m-field>
          </m-col>
        </m-row>
      </m-col>
    </m-row>
    <m-row style="margin-top: 56px" :gx="6" :gy="12">
      <m-col :span="12">
        <m-field label="Địa chỉ">
          <m-input
            v-model="employee.Address"
            @input="handleInput"
            maxlength="255"
          />
        </m-field>
      </m-col>
      <m-col :span="3">
        <m-field label="ĐT di động">
          <m-input
            v-model="employee.PhoneNumber"
            name="PhoneNumber"
            :error="getInputError('PhoneNumber')"
            @blur="handleBlur"
            @input="handleInput"
            maxlength="12"
          />
        </m-field>
      </m-col>
      <m-col :span="3">
        <m-field label="ĐT cố định">
          <m-input
            v-model="employee.TelephoneNumber"
            name="TelephoneNumber"
            :error="getInputError('TelephoneNumber')"
            @blur="handleBlur"
            @input="handleInput"
            maxlength="12"
          />
        </m-field>
      </m-col>
      <m-col :span="3">
        <m-field label="Email">
          <m-input
            v-model="employee.Email"
            name="Email"
            :error="getInputError('Email')"
            @blur="handleBlur"
            @input="handleInput"
            maxlength="255"
          />
        </m-field>
      </m-col>
      <m-col :span="3" />
      <m-col :span="3">
        <m-field label="Số tài khoản">
          <m-input
            v-model="employee.BankAccountNumber"
            name="BankAccountNumber"
            :error="getInputError('BankAccountNumber')"
            @blur="handleBlur"
            @input="handleInput"
            maxlength="50"
          />
        </m-field>
      </m-col>
      <m-col :span="3">
        <m-field label="Tên ngân hàng">
          <m-input
            v-model="employee.BankName"
            @input="handleInput"
            maxlength="120"
          />
        </m-field>
      </m-col>
      <m-col :span="3">
        <m-field label="Chi nhánh">
          <m-input v-model="employee.BankBranchName" @input="handleInput" maxlength="255"/>
        </m-field>
      </m-col>
    </m-row>
    <template #footer>
      <div>
        <m-button color="secondary" @click="$emit('close')">Hủy</m-button>
      </div>
      <div>
        <m-button color="secondary" @click="handleSave()">Cất</m-button>
        &nbsp;
        <m-button @click="handleSaveAndAdd">Cất và thêm</m-button>
      </div>
    </template>
  </m-popup>
  <m-spinner v-if="popupLoading" />
  <m-message-dialog v-bind="messageProps" v-on="messageEvents" />
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
import ApiConfig from "../api-config";
import useForm from "../composables/useForm";
import useMessageDialog from "../composables/useMessageDialog";
import {
  isDateLessThan,
  isEmail,
  isNumber,
  isPhoneNumber,
  isRequired,
} from "../utils/validators";
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
    mode: {
      type: String,
      default: "add",
    },
  },
  setup() {
    const validateDepartment = (dpmId, emp) => {
      if (dpmId) return true;
      return emp.DepartmentName
        ? "Đơn vị không có trong danh sách"
        : "Không được để trống đơn vị";
    };
    const validateEmployeeCode = (code) => {
      return /^NV-\d{1,22}$/.test(code)
        ? true
        : "Mã nhân viên phải theo định dạng NV-X (X là số)";
    };
    const { values: employee, ...rest } = useForm({
      EmployeeCode: [
        isRequired("Không được để trống mã nhân viên"),
        validateEmployeeCode,
      ],
      FullName: [isRequired("Không được để trống họ tên nhân viên")],
      DepartmentId: [validateDepartment],
      DateOfBirth: [
        isDateLessThan(
          "Ngày sinh không được lớn hơn ngày hiện tại",
          new Date()
        ),
      ],
      Email: [isEmail()],
      PhoneNumber: [isPhoneNumber("Số điện thoại di động không hợp lệ")],
      TelephoneNumber: [isPhoneNumber("Số điện thoại cố định không hợp lệ")],
      IdentityDate: [
        isDateLessThan("Ngày cấp không được lớn hơn ngày hiện tại", new Date()),
      ],
      IdentityNumber: [isNumber("Số CMND phải là các chữ số")],
      BankAccountNumber: [isNumber("Số tài khoản phải là các chữ số")],
    });
    return { employee, ...rest, ...useMessageDialog() };
  },
  data() {
    return {
      departments: undefined, //danh sach don vi
      departmentHeaders: [
        { text: "Mã đơn vị", value: "DepartmentCode" },
        { text: "Tên đơn vị", value: "DepartmentName" },
      ], //header cua table don vi trong combobox
      departmentLoading: false, //combobox loading
      popupLoading: false, //co hien spinner ko
    };
  },
  created() {
    switch (this.mode) {
      case "add":
        this.getNewEmployeeCode();
        break;
      case "edit":
        this.loadEmployee();
        break;
      case "clone":
        this.loadEmployee().then(() => this.getNewEmployeeCode());
    }
  },
  methods: {
    closeForm() {
      if (this.isFormChanged) {
        this.showConfirm({
          text: "Dữ liệu đã bị thay đổi. Bạn có muốn cất không?",
          icon: "question",
          showCancel: true,
          onYes: () => this.handleSave(),
          onNo: () => this.$emit("close"),
        });
      } else {
        this.$emit("close");
      }
    },
    //lay ma nv moi
    async getNewEmployeeCode() {
      try {
        const { data: code } = await this.$axios.get(
          ApiConfig.Employee.NEXT_CODE
        );
        this.employee.EmployeeCode = code;
      } catch (err) {
        this.handleAxiosError(err);
      }
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
        .get(ApiConfig.Department.BASE)
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
      this.errors.DepartmentId = null;
    },
    //ham load nhan vien (form sua)
    async loadEmployee() {
      this.popupLoading = true;
      try {
        const { data: emp } = await this.$axios.get(
          `${ApiConfig.Employee.BASE}/${this.employeeId}`
        );
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
    //ham lay loi dau tien
    getFirstError() {
      const fk = Object.keys(this.errors).find((k) => this.errors[k]);
      return this.errors[fk];
    },
    //ham luu thong tin nv
    async handleSave(preventClose = false) {
      if (!this.validate()) {
        this.showError({ text: this.getFirstError() });
        return;
      }
      this.popupLoading = true;
      let hasError = false;
      try {
        if (this.mode == "edit") {
          //neu la form sua
          await this.$axios.put(
            `${ApiConfig.Employee.BASE}/${this.employeeId}`,
            this.employee
          );
        } else {
          //form them
          await this.$axios.post(ApiConfig.Employee.BASE, this.employee);
        }
      } catch (error) {
        hasError = true;
        this.handleAxiosError(error);
      } finally {
        this.popupLoading = false;
      }
      if (!preventClose && !hasError) {
        this.$emit("close", 1);
      }
    },
    //bat su kien click nut cat va them
    async handleSaveAndAdd() {
      await this.handleSave(true);
      this.reset();
      this.getNewEmployeeCode();
      this.$emit("saveAndAdd");
    },
    //ham xu ly loi tu axios
    handleAxiosError(err) {
      console.log(err.response);
      let userMsg = err.response?.data?.userMessage;
      if (!userMsg) {
        userMsg = "Có lỗi xảy ra. Vui lòng liên hệ Misa để được hỗ trợ.";
      }
      this.showError({ text: userMsg });
    },
  },
};
</script>
