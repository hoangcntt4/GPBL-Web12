<template>
  <div class="m-dialog">
    <div class="dialog-content">
      <div class="popup-header">
        <!-- title -->
        <div class="popup-title">
          <div class="popup-title-container">
            <div class="title__popup">Thông tin nhân viên</div>
            <div class="cbo">
              <div class="checkbox">
                <base-checkbox></base-checkbox>
                <div>Là khách hàng</div>
              </div>
              <div class="checkbox">
                <base-checkbox></base-checkbox>
                <div>Là nhà cung cấp</div>
              </div>
            </div>
          </div>
        </div>
        <!-- icon-support  -->
        <div class="popup-support">
          <base-icon icon="help"></base-icon>
          <base-icon icon="close" @click="closeFormDetail()"></base-icon>
        </div>
      </div>
      <!-- content: form -->
      <div class="popup-content-container">
        <!-- form chi tiết  -->
        <form>
          <div class="popup-content">
            <div class="pop-top">
              <!-- form-left  -->
              <div class="e-left">
                <div class="top-e-ip-1">
                  <!-- Mã  -->
                  <div class="top-e-ip-1-left" style="padding-right: 6px">
                    <div class="ip-text">
                      <span>Mã</span>
                      <span class="require-field">&nbsp;*</span>
                    </div>
                    <div class="ip-e-employeeCode">
                      <base-input
                        v-model="employee.employeeCode"
                        rule="required"
                        type="text"
                        :isRemoveErr="removeErr"
                        autoFocus
                        :titleErr="errors.employeeCode"
                        idField="txtEmployeeCode"
                      />
                    </div>
                  </div>
                  <!-- Tên  -->
                  <div class="top-e-ip-1-right">
                    <div class="ip-text">
                      <span>Tên</span>
                      <span class="require-field">&nbsp;*</span>
                    </div>
                    <div class="ip-e-fullname">
                      <base-input
                        v-model="employee.fullName"
                        rule="required"
                        type="text"
                        :isRemoveErr="removeErr"
                        :titleErr="errors.employeeName"
                        idField="txtFullName"
                      />
                    </div>
                  </div>
                </div>
                <!-- Đơn vị  -->
                <div class="top-e-ip-2">
                  <div class="ip-text">
                    <span>Đơn vị</span>
                    <span class="require-field">&nbsp;*</span>
                  </div>
                  <div>
                    <!-- Combobox đơn vị -->
                    <template>
                      <div>
                        <multiselect
                          v-model="department"
                          :options="options"
                          placeholder=""
                          label="name"
                          track-by="name"
                          :showNoResults="false"
                          :showNoOptions="false"
                          :class="{ 'is-invalid': errorCbo }"
                        >
                        </multiselect>
                      </div>
                    </template>
                  </div>
                </div>
                <!-- Chức danh  -->
                <div class="top-e-ip-3">
                  <div class="ip-text">
                    <span>Chức danh</span>
                  </div>
                  <div>
                    <base-input type="text" v-model="employee.positionName" />
                  </div>
                </div>
              </div>
              <!-- form-right  -->
              <div class="e-right">
                <div class="top-e-ip-1">
                  <!-- Ngày sinh  -->
                  <div class="top-e-ip-1-left" style="padding-right: 6px">
                    <div class="ip-text">
                      <span>Ngày sinh</span>
                    </div>
                    <div style="width: 161px">
                      <base-input type="date" v-model="employee.dateOfBirth" />
                    </div>
                  </div>
                  <!-- Giới tính  -->
                  <div class="top-e-ip-1-right" style="padding-left: 10px">
                    <div class="ip-text">
                      <span>Giới tính</span>
                    </div>
                    <div class="gender-radio">
                      <div>
                        <base-radio :option="1" v-model="employee.gender"
                          >Nam</base-radio
                        >
                      </div>
                      <div>
                        <base-radio :option="0" v-model="employee.gender"
                          >Nữ</base-radio
                        >
                      </div>
                      <div>
                        <base-radio :option="2" v-model="employee.gender"
                          >Khác</base-radio
                        >
                      </div>
                    </div>
                  </div>
                </div>
                <!-- CMND  -->
                <div class="top-e-ip-2" style="display: flex">
                  <!-- Số CMND  -->
                  <div style="padding-right: 6px">
                    <div class="ip-text">
                      <span>Số CMND</span>
                    </div>
                    <div class="ip-e-identityNumber">
                      <base-input
                        type="text"
                        v-model="employee.identityNumber"
                        idField="txtIdentityNumber"
                      />
                    </div>
                  </div>
                  <!-- Ngày cấp  -->
                  <div>
                    <div class="ip-text">
                      <span>Ngày cấp</span>
                    </div>
                    <div>
                      <input
                        type="date"
                        class="date-input"
                        style="width: 168px"
                        v-model="employee.identityDate"
                      />
                      <!-- <base-input v-model="employee.identityDate" /> -->
                    </div>
                  </div>
                </div>
                <!-- Nơi cấp  -->
                <div class="top-e-ip-3">
                  <div class="ip-text">
                    <span>Nơi cấp</span>
                  </div>
                  <div>
                    <base-input type="text" v-model="employee.identityPlace" />
                  </div>
                </div>
              </div>
            </div>
            <div class="pop-bot">
              <div class="pop-bot-info">
                <div class="info-container">
                  <div class="info-detail">
                    <!-- Địa chỉ  -->
                    <div>
                      <div class="ip-text">
                        <span>Địa chỉ</span>
                      </div>
                      <div>
                        <base-input type="text" v-model="employee.address" />
                      </div>
                    </div>
                    <div class="info-contact">
                      <!-- DT di động  -->
                      <div>
                        <div class="ip-text">
                          <span>ĐT di động</span>
                        </div>
                        <div>
                          <base-input
                            type="text"
                            v-model="employee.telephoneNumber"
                          />
                        </div>
                      </div>
                      <!-- ĐT cố định  -->
                      <div>
                        <div class="ip-text">
                          <span>ĐT cố định</span>
                        </div>
                        <div>
                          <base-input
                            type="text"
                            v-model="employee.phoneNumber"
                          />
                        </div>
                      </div>
                      <!-- Email  -->
                      <div>
                        <div class="ip-text">
                          <span>Email</span>
                        </div>
                        <div>
                          <base-input type="text" v-model="employee.email" />
                        </div>
                      </div>
                    </div>

                    <div class="info-contact">
                      <!-- Tài khoản ngân hàng  -->
                      <div>
                        <div class="ip-text">
                          <span>Tài khoản ngân hàng</span>
                        </div>
                        <div>
                          <base-input
                            type="text"
                            v-model="employee.bankAccountNumber"
                          />
                        </div>
                      </div>
                      <!-- Tên ngân hàng  -->
                      <div>
                        <div class="ip-text">
                          <span>Tên ngân hàng</span>
                        </div>
                        <div>
                          <base-input type="text" v-model="employee.bankName" />
                        </div>
                      </div>
                      <!-- Chi nhánh  -->
                      <div>
                        <div class="ip-text">
                          <span>Chi nhánh</span>
                        </div>
                        <div>
                          <base-input
                            type="text"
                            v-model="employee.bankBranchName"
                          />
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </form>

        <div class="divide"></div>
        <div class="popup-footer-container">
          <div class="popup-footer">
            <!-- Nút hủy: ko lưu dữ liệu và đóng form -->
            <div>
              <base-button
                color="secondary"
                size="sm"
                @click="closeFormDetail()"
                >Hủy</base-button
              >
            </div>
            <div class="footer-btn-right">
              <!-- Nút cất: Lưu dữ liệu và đóng form -->
              <div style="padding: 0 10px">
                <base-button
                  color="secondary"
                  size="sm"
                  @click="btnSaveEmployee()"
                  >Cất</base-button
                >
              </div>
              <!-- Nút Cất và thêm: Lưu dữ liệu, vẫn hiển thị form và reset form -->
              <div>
                <base-button size="sm" @click="btnSaveEmployee(1)"
                  >Cất và Thêm</base-button
                >
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>

    <!-- dialog validate  -->
    <base-dialog
      v-if="isShowDialogValidate"
      icon="error"
      :text="errorMsg"
      notic
      @close="closeDialogValidate()"
    />

    <!-- dialog trùng mã nv -->
    <base-dialog
      v-if="isShowDialogDuplicate"
      icon="warning"
      :text="duplicateMsg"
      ok
      @close="closeDialogDuplicate()"
    />

    <!-- dialog cảnh báo thay đổi dữ liệu -->
    <base-dialog
      v-if="isShowDialogChange"
      change
      icon="question"
      text="Dữ liệu đã bị thay đổi. Bạn có muốn cất không?"
      confirm
      @close="closeDialogDuplicate()"
    />

    <toast-message v-if="isShowToast" />

    <!-- loading -->
    <base-loading v-if="isLoading" />
  </div>
</template>

<script>
import axios from "axios";
import resource from "../../resource/resource.js";
import BaseIcon from "../../components/base/BaseIcon.vue";
import BaseInput from "../../components/base/BaseInput.vue";
import BaseButton from "../../components/base/BaseButton.vue";
import BaseCheckbox from "../../components/base/BaseCheckbox.vue";
import BaseRadio from "../../components/base/BaseRadio.vue";
import Multiselect from "vue-multiselect";
import BaseLoading from "../../components/base/BaseLoading.vue";
import BaseDialog from "../../components/base/BaseDialog.vue";
import ToastMessage from "../../components/base/ToastMessage.vue"
export default {
  components: {
    BaseIcon,
    Multiselect,
    BaseLoading,
    BaseInput,
    BaseCheckbox,
    BaseRadio,
    BaseButton,
    BaseDialog,
    ToastMessage
  },
  name: "DetailEmployee",
  props: ["employeeId", "editMode"],
  data: () => ({
    //thông tin nhân viên
    employee: {
      employeeCode: null,
      fullName: null,
      departmentId: null,
      positionName: null,
      dateOfBirth: null,
      gender: null,
      identityNumber: null,
      identityDate: null,
      identityPlace: null,
      address: null,
      telephoneNumber: null,
      phoneNumber: null,
      email: null,
      bankAccountNumber: null,
      bankName: null,
      bankBranchName: null,
    },
    //object: dùng để hứng giá trị của props employeeId và editMode
    typeSave: {
      employeeId: "",
      editMode: "",
    },
    //nhận giá trị cho combobox đơn vị
    department: {},
    //array: danh sách item trong combobox đơn vị
    options: [],
    //object lưu lỗi của validate các filed input
    errors: {
      employeeCode: "",
      fullName: "",
      departmentId: "",
      identityNumber: "",
    },
    checkLoad: false, //boolean: check load lại dữ liệu
    isShowDialogValidate: false, //boolean: ẩn hiện dialog validate dữ liệu
    isShowDialogDuplicate: false, //boolean: ẩn-hiện dialog trùng mã nv
    errorMsg: "", //thông báo lỗi
    isLoading: false, //boolean: ẩn hiện loading
    employeeCheck: {}, //object: so sánh employee
    closeForm: true, //boolean: đóng form chi tiết
    removeErr: false, //boolean: clear errors provider
    errorCbo: false, //boolean: check lỗi combobox
    isShowDialogChange: false, //boolean: ẩn/hiện dialog thông báo thay đổi
    isShowToast: false, //boolean: ẩn/ hiện toast message
  }),

  created() {
    //gán giá trị của props employeeId và editMode cho typeSave:
    this.typeSave.employeeId = this.employeeId;
    this.typeSave.editMode = this.editMode;
    //Lấy dữ liệu department
    this.getDepartment();
    //check employeeId:
    if (this.typeSave.employeeId) {
      //lấy thông tin nhân viên theo Id
      this.getEmployee(this.typeSave.employeeId);
    } else {
      //làm mới form chi tiết
      this.refreshFormDetail();
    }
    
  },
  watch: {
    /**
     - Theo dõi department để bắt lỗi field departmentId
     - Author: LQTrung (13/02/2022)
     */
    department() {
      if (!this.department) {
        this.errorCbo = true;
      } else {
        this.errorCbo = false;
      }
    },
  },
  methods: {
    /**
     - Làm mới form chi tiết
     - Author: LQTrung (13/02/2022)
     */
    refreshFormDetail() {
      //lấy mã nhân viên tự tăng
      this.getNextEmployeeCode();
      let eCode = this.employee.employeeCode;
      //làm mới employee
      this.employee = {
        employeeCode: eCode,
        gender: 1,
      };
      this.department = {};
      this.removeErr = false;
    },

    /**
     - Lấy dữ liệu nhân viên theo Id
     - Author: LQTrung (13/02/2022)
     */
    getEmployee(value) {
      var me = this;
      //hiện thị loading
      me.isLoading = true;
      axios
        .get(`/employees/${value}`)
        .then(function (res) {
          //gán dữ liệu response cho employee
          me.employee = res.data;
          //check editMode để chọn trạng thái nhân bản
          if (me.editMode == 1) {
            me.getNextEmployeeCode();
          }
          //format ngày sinh và ngày cấp CMND
          me.employee.dateOfBirth = resource.formatDate(res.data.dateOfBirth,1);
          me.employee.identityDate = resource.formatDate(res.data.identityDate,1);
          //Lấy ra đơn vị theo departmentId
          axios
            .get(`departments/${me.employee.departmentId}`)
            .then(function (res) {
              //gán dữ liệu response cho department
              me.department = {
                id: res.data.departmentId,
                name: res.data.departmentName,
              };
            });
          //ẩn loading
          me.isLoading = false;
        })
        .catch(function (err) {
          //ẩn loading
          me.isLoading = false;
          me.catchAxiosError(err);
        });
    },

    /**
     - Lấy mã nhân viên tự tăng
     - Author: LQTrung (13/02/2022)
     */
    getNextEmployeeCode() {
      var me = this;
      axios
        .get("employees/next-code")
        .then(function (res) {
          me.employee.employeeCode = res.data;
        })
        .catch(function (err) {
          me.catchAxiosError(err);
        });
    },

    /**
     - Lấy dữ liệu đổ vào combobox đơn vị
     - Author: LQTrung (13/02/2022)
     */
    getDepartment() {
      var me = this;
      //gọi api lấy ra tất cả đơn vị
      axios
        .get("/departments")
        .then(function (res) {
          var dpName = res.data;
          //thêm đối tượng gồm departmentId và departmentName vào options
          dpName.forEach((dp) => {
            me.options.push({ id: dp.departmentId, name: dp.departmentName });
          });
        })
        .catch(function (err) {
          me.catchAxiosError(err);
        });
    },

    /**
     - Validate dữ liệu
     - Author: LQTrung (13/02/2022)
     */
    validate() {
      let isValid = true;
      this.errors = {
        employeeCode: "",
        fullName: "",
        departmentId: "",
        identityNumber: "",
      };
      this.errorMsg = "";
      //Ngày cấp không được lớn hơn ngày hiện tại
      var identitydate = this.employee.identityDate;
      if (identitydate != null && this.validateDate(identitydate) == false) {
        this.errorMsg = resource.VI.errorMsgDob;
        isValid = false;
      }
      //ngày sinh không được lớn hơn ngày hiện tại
      var dob = this.employee.dateOfBirth;
      if (dob != null && this.validateDate(dob) == false) {
        this.errorMsg = resource.VI.errorMsgDob;
        isValid = false;
      }
      //Mã đơn vị không được để trống
      if (this.department.id) {
        //gán dữ liệu field combobox đơn vị cho employee.department
        this.employee.departmentId = this.department.id;
      }
      if (!this.employee.departmentId) {
        this.errorMsg = resource.VI.errorMsgDepartment;
        isValid = false;
      }
      //Tên nhân viên không được để trống
      if (!this.employee.fullName) {
        this.errors.fullName = this.errorMsg = resource.VI.errorMsgFullName;
        isValid = false;
      }
      //Mã nhân viên không được để trống
      var eCode = this.employee.employeeCode;
      if (!eCode) {
        this.errors.employeeCode = this.errorMsg =
          resource.VI.errorEmployeeCodeEmpty;
        isValid = false;
      } else {
        //định dạng employcode NV-x
        if (!this.validateEmployeeCode(eCode)) {
          this.errors.employeeCode = this.errorMsg =
            resource.VI.errorEmployeeCode;
          isValid = false;
        }
      }
      return isValid;
    },

    /**
     - Button lưu dữ liệu
     - Author: LQTrung (13/02/2022)
     */
    btnSaveEmployee(type) {
      if (this.validate()) {
        //chuyển filed gới tính sang kiểu số
        this.employee.gender = new Number(this.employee.gender);
        //clear lỗi các ô input
        this.removeErr = !this.removeErr;
        switch (this.typeSave.editMode) {
          case 0: //sửa thông tin nhân viên
            this.updateEmployee(type);
            break;
          case 1: //thêm nhân viên
            this.addNewEmployee(type);
            break;
          case 2: //
          default:
            break;
        }
        document.getElementById("txtEmployeeCode").focus();
      } else {
        this.isShowDialogValidate = true;
      }
    },

    /**
     - Thực hiện thêm mới nhân viên
     - Author: LQTrung (13/02/2022)
     */
    addNewEmployee(type) {
      var me = this;
      //hiện thị loading
      me.isLoading = true;
      //gọi API thực hiện thêm nhân viên
      axios
        .post("/employees", this.employee)
        .then(function (res) {
          console.log(res);
          if (type != 1) {
            //close form chi tiết
            me.closeFormDetail();
          }
          //Truyền dữ liệu tới component cha: ListEmployee
          me.checkLoad = true;
          me.$emit("loadData", me.checkLoad);
          //ẩn loading
          me.isLoading = false;
          //làm mới form chi tiết
          me.refreshFormDetail();
        })
        .catch(function (err) {
          //ẩn loading
          me.isLoading = false;
          //Check trùng mã
          if (err.response.data.userMsg == resource.VI.userMsgDuplicate) {
            me.checkDuplicate(me.employee.employeeCode);
          } else {
            me.catchAxiosError(err);
          }
        });
    },

    /**
     - Thực hiện cập nhật thông tin nhân viên
     - Author: LQTrung (13/02/2022)
     */
    updateEmployee(type) {
      var me = this;
      //hiện thị loading
      me.isLoading = true;
      //gọi API thực hiện sửa nhân viên
      axios
        .put(`/employees/${this.employee.employeeId}`, this.employee)
        .then(function (res) {
          console.log(res);
          if (type != 1) {
            //close form chi tiết
            me.closeFormDetail();
          }
          //Truyền dữ liệu tới component cha: ListEmployee
          me.checkLoad = true;
          me.$emit("loadData", me.checkLoad);

          //ẩn loading
          me.isLoading = false;
          //làm mới form chi tiết
          me.typeSave.employeeId = "";
          me.typeSave.editMode = 1;
          me.refreshFormDetail();
        })
        .catch(function (err) {
          //ẩn loading
          me.isLoading = false;
          //Check trùng mã
          if (err.response.data.userMsg == resource.VI.userMsgDuplicate) {
            me.checkDuplicate(me.employee.employeeCode);
          } else {
            me.catchAxiosError(err);
          }
        });
      //ẩn loading
      me.isLoading = false;
    },

    /**
     - Ẩn form chi tiết
     - Author: LQTrung (13/02/2022)
     */
    closeFormDetail() {
      this.$emit("closeForm", this.closeForm);
      this.employee = {
        gender: 1,
      };
      this.errors = false;
      this.department = {};
      this.removeErr = !this.removeErr;
    },

    /**
     - Ẩn dialog validate
     - Author: LQTrung (13/02/2022)
     */
    closeDialogValidate() {
      this.isShowDialogValidate = false;
      if (this.errors.employeeCode) {
        document.getElementById("txtEmployeeCode").focus();
      } else if (this.errors.fullName) {
        document.getElementById("txtFullName").focus();
      } else if (!this.employee.departmentId) {
        this.errorCbo = true;
      }
    },

    /**
     - Ẩn dialog trùng mã
     - Author: LQTrung (13/02/2022)
     */
    closeDialogDuplicate() {
      this.isShowDialogDuplicate = false;
    },

    /**
     - Validate ngày tháng
     - Author: LQTrung (13/02/2022)
     */
    validateDate(d) {
      var date = new Date(d);
      if (date > new Date()) {
        return false;
      }
      return true;
    },

    /**
     - Validate employeeCode
     - Author: LQTrung (13/02/2022)
     */
    validateEmployeeCode(code) {
      return /^NV-\d{1,22}$/.test(code) ? true : false;
    },

    /**
     - Check trùng mã nhân viên
     */
    checkDuplicate(eCode) {
      this.duplicateMsg = resource.VI.msgDuplicate(eCode);
      this.isShowDialogDuplicate = true;
    },

    /**
     - Thông báo lỗi axios
     - Author: LQTrung (13/02/2022)
     */
    catchAxiosError(err) {
      console.log(err);
      this.errorMsg = resource.VI.errorMsg;
      this.isShowDialogValidate = true;
    },
  },
};
</script>

<style scoped src="../../assets/css/views/employeeDetail.css"></style>
