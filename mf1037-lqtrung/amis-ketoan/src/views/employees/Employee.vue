<template>
  <div id="lstEmployees">
    <div class="lstemployees-header">
      <div class="title-employee">Nhân viên</div>
      <!-- button thêm mới: hiển thị form nhân viên -->
      <div class="btnAddEmployee">
        <base-button @click="btnAddEmployee()">Thêm mới nhân viên</base-button>
      </div>
    </div>
    <div class="lstEmployees-content">
      <div class="employees-search">
        <div class="filed-left">
          <!-- button thực hiện hàng loạt -->
          <base-button color="high" @click="showContextDelete()"
            >Thực hiện hàng loạt
          </base-button>
          <!-- context xóa nhiều nhân viên -->
          <div class="gop" v-if="gop">
            <ul>
              <li @click="confirmDeleteMany()">Xóa</li>
            </ul>
          </div>
        </div>

        <div class="filed-right">
          <!-- input tìm kiếm nhân viên -->
          <div class="search">
            <div class="ms-input">
              <base-input
                v-model="key_search"
                placeholder="Tìm theo mã, tên nhân viên"
                icon="search"
                autoFocus
              />
            </div>
          </div>
          <!-- button reload dữ liệu -->
          <div class="reload">
            <base-icon icon="refresh" @click="reloadData()"></base-icon>
            <base-icon icon="excel" @click="exportExcel()"></base-icon>
          </div>
        </div>
      </div>
      <!-- bảng dữ liệu danh sách nhân viên -->
      <div class="employees-table">
        <table id="tbEmployees">
          <thead>
            <th style="width: 40px" class="text-center">
              <div style="margin-top: 4px">
                <base-checkbox
                  inputValue="checked"
                  v-model="checkedAll"
                ></base-checkbox>
              </div>
            </th>
            <th class="text-left" style="width: 134px !important">
              MÃ NHÂN VIÊN
            </th>
            <th class="text-left">TÊN NHÂN VIÊN</th>
            <th class="text-left" style="width: 105px">GIỚI TÍNH</th>
            <th class="text-center" style="width: 150px">NGÀY SINH</th>
            <th class="text-left" style="width: 120px">SỐ CMND</th>
            <th class="text-left" style="width: 155px">CHỨC DANH</th>
            <th class="text-left" style="width: 150px">TÊN ĐƠN VỊ</th>
            <th class="text-center">CHỨC NĂNG</th>
          </thead>
          <tbody>
            <tr
              v-for="employee in employees"
              :key="employee.employeeId"
              @dblclick="showFormDetail(employee.EmployeeId)"
            >
              <td class="text-center">
                <div style="margin-top: 4px">
                  <base-checkbox
                    :inputValue="employee.EmployeeId"
                    v-model="checkboxArray"
                  ></base-checkbox>
                </div>
              </td>
              <td class="text-left">{{ employee.EmployeeCode }}</td>
              <td class="text-left">{{ employee.FullName }}</td>
              <td class="text-left">{{ employee.Gender }}</td>
              <td class="text-center">
                {{ employee.DateOfBirth }}
              </td>
              <td class="text-left">{{ employee.IdentityNumber }}</td>
              <td class="text-left">{{ employee.PositionName }}</td>
              <td class="text-left">{{ employee.DepartmentName }}</td>
              <td class="text-center">
                <div class="fnc_btn">
                  <div>
                    <!-- button sửa thông tin nhân viên -->
                    <button
                      class="btn_fix"
                      @click="showFormDetail(employee.EmployeeId)"
                    >
                      <div class="txtBtnFix">Sửa</div>
                    </button>
                    <!-- button hiện context menu -->
                    <button
                      class="btn_arrow"
                      @click="showContextMenu(employee.EmployeeId)"
                    >
                      <base-icon :size="16" icon="arrow-up" />
                    </button>
                  </div>
                </div>
                <!-- context Menu -->
                <div
                  class="context-menu"
                  :id="employee.EmployeeId"
                  @mouseleave="closeContexMenu(employee.EmployeeId)"
                >
                  <ul>
                    <li @click="clonedEmployee(employee.EmployeeId)">
                      Nhân bản
                    </li>
                    <li
                      @click="
                        confirmDelete(
                          employee.EmployeeId,
                          employee.EmployeeCode
                        )
                      "
                    >
                      Xóa
                    </li>
                    <li>Ngừng sử dụng</li>
                  </ul>
                </div>
              </td>
            </tr>
            <div class="no-data" v-if="totalRecord == 0">
              <img src="../../assets/img/no-content.svg" alt="no-content" />
              <b></b>
              <p>Không có dữ liệu</p>
            </div>
          </tbody>
          <tfoot></tfoot>
        </table>
      </div>
      <div class="ms-pagination">
        <div class="left-pagination">
          <div class="total-record">
            Tổng số: <b>{{ totalRecord }}</b> bản ghi
          </div>
        </div>
        <div class="right-pagination">
          <div class="record-in-page">
            <!-- combobox pagesize -->
            <div class="cbo-pageSize">
              <multiselect
                v-model="pageSizeCbo"
                :options="options"
                placeholder="Select one"
                label="name"
                track-by="name"
                :searchable="false"
                :allow-empty="false"
                class="pagesize"
              ></multiselect>
            </div>
          </div>
          <!-- phân trang -->
          <div class="page-number">
            <div class="front-page">
              <button @click="btnIndexPaging(pageIndex - 1)">Trước</button>
            </div>
            <div class="page-index">
              <div>
                <button
                  :class="pageIndex == 1 ? 'pageSelected' : ''"
                  @click="btnIndexPaging(1)"
                >
                  <span>1</span>
                </button>
                <button v-if="num > 4" @click="num -= 3">
                  <span>...</span>
                </button>
              </div>
              <div v-for="n in totalPageNumber" :key="n">
                <div>
                  <button
                    @click="btnIndexPaging(n)"
                    :class="n == pageIndex ? 'pageSelected' : ''"
                    v-if="
                      (n == num - 1 && n != 1) ||
                      n == num ||
                      n == num + 1 ||
                      (n == totalPageNumber && n != 1)
                    "
                  >
                    <span>{{ n }}</span>
                  </button>
                  <div v-else>
                    <button v-if="n == num + 2" @click="num += 3">
                      <span>...</span>
                    </button>
                  </div>
                </div>
              </div>
            </div>
            <div class="back-page">
              <button @click="btnIndexPaging(pageIndex + 1)">Sau</button>
            </div>
          </div>
        </div>
      </div>
    </div>

    <!-- dialog chi tiết nhân viên  -->
    <employee-detail
      v-if="isShowDetailEmployee"
      :employeeId="employeeId"
      :editMode="editMode"
      @loadData="loadData"
      @closeForm="closeFormDetail"
    />

    <!-- dialog xác nhận xóa nhân viên -->
    <base-dialog
      v-if="isShowDialogDel"
      icon="warning"
      confirm
      :text="msgDelete"
      @no="isShowDialogDel = false"
      @yes="deleteEmployees()"
    />

    <!-- dialog validate  -->
    <base-dialog
      v-if="isShowDialogErrMsg"
      icon="error"
      :text="errorMsg"
      notic
      @close="isShowDialogErrMsg = false"
    />

    <toast-message v-if="isShowToast" />

    <!-- loading -->
    <base-loading v-if="isLoading" />
  </div>
</template>
<script>
import BaseIcon from "../../components/base/BaseIcon.vue";
import BaseButton from "../../components/base/BaseButton.vue";
import BaseInput from "../../components/base/BaseInput.vue";
import BaseCheckbox from "../../components/base/BaseCheckbox.vue";
import BaseLoading from "../../components/base/BaseLoading.vue";
import Multiselect from "vue-multiselect";
import BaseDialog from "../../components/base/BaseDialog.vue";
import EmployeeDetail from "../../views/employees/EmployeeDetail.vue";
import ToastMessage from "../../components/base/ToastMessage.vue";
import axios from "axios";
import resource from "../../resource/resource.js";
export default {
  name: "Employees",
  components: {
    BaseLoading,
    Multiselect,
    BaseIcon,
    BaseButton,
    BaseInput,
    BaseCheckbox,
    BaseDialog,
    EmployeeDetail,
    ToastMessage,
  },
  data: () => ({
    //array: danh sách nhân viên
    employees: [],
    //data để truyển xuống EmployeeDetail
    employeeId: null,
    editMode: null,

    isShowDetailEmployee: false, //boolean: ẩn/hiện form chi tiết nhân viên
    key_search: "", //từ khóa tìm kiếm
    gop: false, //boolean: ẩn/hiện context menu xóa nhiều
    eIdDelete: null, //mã nhân viên bị xóa
    deleteMore: false, //boolean: check xem có phải trạng tháng xóa nhiều không
    totalRecord: 0, //tổng số bản nhân viên
    totalPageNumber: 1, //tổng số trang
    pageIndex: 1, //ví trí trang
    pageSize: 10, //số nhân viên trên 1 trang
    num: 2, //dùng trong phần phân trang
    isLoading: false, //boolean: ẩn/hiện loading dữ liệu
    isShowDialogDel: false, //boolean: ẩn/hiện dialog xóa nhân viên
    msgDelete: "", //thông báo xóa nhân viên
    isShowDialogErrMsg: false, //boolean: ẩn/hiện dialog thông báo lỗi
    errorMsg: "", //thông báo lỗi
    checkboxArray: [], //array: chứa các nhân viên được đánh dấu
    checkedAll: [], //array: dùng để đánh dấu tất cả nhân viên trong bảng
    //combobox phân trang
    options: [
      { name: "10 bản ghi trên 1 trang", value: 10 },
      { name: "20 bản ghi trên 1 trang", value: 20 },
      { name: "30 bản ghi trên 1 trang", value: 30 },
      { name: "50 bản ghi trên 1 trang", value: 50 },
      { name: "100 bản ghi trên 1 trang", value: 100 },
    ],
    //giá trị mặc định
    pageSizeCbo: { name: "10 bản ghi trên 1 trang", value: 10 },
    isShowToast: false, //ẩn/hiện toast message
  }),
  created() {
    // lấy dữ liệu nhân viên
    this.getData();
  },
  watch: {
    /**
    - Theo dõi sự thay đổi của key_search để tìm kiếm nhân viên
    - Author: LQTrung (08/02/2022)
     */
    key_search() {
      this.pageIndex = 1;
      this.num = 2;
      this.getData();
    },

    /**
    - Theo dõi sự thay đổi của checkedAll
    - Author: LQTrung (08/02/2022)
     */
    checkedAll() {
      if (this.checkedAll[0] == "checked") {
        //add từng employeeId vào checkboxArray
        this.employees.forEach((e) => {
          this.checkboxArray.push(e.EmployeeId);
        });
        return 10;
      } else {
        this.checkboxArray = [];
        return 0;
      }
    },

    /**
    - Combobox thay đổi pagesize
    - Author: LQTrung (08/02/2022)
     */
    pageSizeCbo(val) {
      this.pageIndex = 1;
      this.pageSize = val.value;
      this.getData();
    },
  },
  methods: {
    /**
     - Check data nhận từ component DetailEmployee để load lại dữ liệu
     - Author: LQTrung (08/02/2022)
     */
    loadData(checkLoad) {
      if (checkLoad == true) {
        this.isShowToast = true;
        setTimeout(() => {
          this.isShowToast = false;
        }, 3000);
        this.getData();
      }
    },

    /**
     - Thực hiện lấy dữ liệu nhân viên
     - Author: LQTrung (08/02/2022)
     */
    getData() {
      var me = this;
      //hiện thị loading
      me.isLoading = true;
      //Gọi api lấy dữ liệu danh sách nhân viên
      axios
        .get(
          `employees/paging?searchText=${this.key_search}&pageIndex=${this.pageIndex}&pageSize=${this.pageSize}`
        )
        .then(function (res) {
          //gán dữ liệu trả về cho employees
          me.employees = res.data.data;
          me.employees.forEach((e) => {
            e.DateOfBirth = resource.formatDate(e.DateOfBirth);
            e.Gender = resource.formatGender(e.Gender);
          });
          //gán dữ liệu cho tổng số nhân viên
          me.totalRecord = res.data.totalRecord;
          //tính toán tổng số trang
          var n = me.totalRecord / me.pageSize;
          me.totalPageNumber = n > parseInt(n) ? parseInt(n) + 1 : n;
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
     - Reload dữ liệu
     - Author: LQTrung (08/02/2022)
     */
    reloadData() {
      this.key_search = "";
      this.pageIndex = 1;
      this.checkedAll = [];
      this.getData();
    },

    /**
     - Hiển thị form thêm mới nhân viên
     - Author: LQTrung (09/02/2022)
     */
    btnAddEmployee() {
      this.isShowDetailEmployee = true;
      this.editMode = 1;
      this.employeeId = null;
    },

    /**
     - Hiển thị form chi tiết nhân viên
     - Author: LQTrung (09/02/2022)
     */
    showFormDetail(eId) {
      //hiển thị form chi tiết
      this.isShowDetailEmployee = true;
      this.editMode = 0;
      //gán employeeId trong data = employeeId từ tr
      this.employeeId = eId;
    },

    /**
     - Đóng form chi tiết nhân viên
     - Author: LQTrung (09/02/2022)
     */
    closeFormDetail(value) {
      if (value) {
        this.isShowDetailEmployee = false;
        this.editMode = null;
        this.employeeId = null;
      }
    },

    /**
     - Hiển thị context menu
     - Author: LQTrung (09/02/2022)
     */
    showContextMenu(eId) {
      document.getElementById(eId).style.display = "block";
    },
    /**
     - Ẩn context menu
     - Author: LQTrung (09/02/2022)
     */
    closeContexMenu(eId) {
      document.getElementById(eId).style.display = "none";
    },

    /**
     - Nhân bản nhân viên
     - Author: LQTrung (10/02/2022)
     */
    clonedEmployee(eId) {
      this.isShowDetailEmployee = true;
      this.editMode = 1;
      this.employeeId = eId;
    },

    /**
     - Hiện dialog xác nhận xóa
     - Author: LQTrung (10/02/2022)
     */
    confirmDelete(eId, eCode) {
      this.eIdDelete = eId;
      this.msgDelete = resource.VI.msgDelete(eCode);
      this.isShowDialogDel = true;
    },

    /**
     - Thực hiện xóa nhân viên
     - Author: LQTrung (10/02/2022)
     */
    deleteEmployees() {
      var me = this;
      me.isLoading = true;
      //hiện thị loading
      if (this.deleteMore == true) {
        axios
          .post("employees/delete-many", this.checkboxArray)
          .then(function (res) {
            console.log(res);
            me.deleteMore = false;
            me.isShowDialogDel = false;
            me.isShowToast = true;
            setTimeout(() => {
              me.isShowToast = false;
            }, 3000);
            me.getData();
          })
          .catch(function (err) {
            me.catchAxiosError(err);
          });
      } else {
        //gọi API xóa dữ liệu 1 nhân viên
        axios
          .delete(`/Employees/${this.eIdDelete}`)
          .then(function (res) {
            console.log(res);
            me.isShowDialogDel = false;
            me.isShowToast = true;
            setTimeout(() => {
              me.isShowToast = false;
            }, 3000);
            me.getData();
          })
          .catch(function (err) {
            me.catchAxiosError(err);
          });
      }
      //ẩn loading
      me.isLoading = false;
    },

    /**
     - Hiện context xóa nhiều nhân viên
     - Author: LQTrung (11/02/2022)
     */
    showContextDelete() {
      if (this.checkboxArray.length > 0) {
        this.gop = true;
      }
    },

    /**
     - Hiện dialog xác nhận xóa nhiều nhân viên
     - Author: LQTrung (11/02/2022)
     */
    confirmDeleteMany() {
      this.deleteMore = true;
      this.gop = false;
      this.msgDelete = resource.VI.msgDeleteMany;
      this.isShowDialogDel = true;
    },

    /**
     - Phân trang dữ liệu theo số thứ tự trang
     - Author: LQTrung (12/02/2022)
     */
    btnIndexPaging(index) {
      this.pageIndex = index;
      this.getData();
    },

    /**
     - Xuất file excel danh sách nhân viên
     - Author: LQTrung (12/02/2022)
     */
    exportExcel() {
      location.href = axios.defaults.baseURL + "employees/export";
    },

    /**
     - Thông báo lỗi axios
     - Author: LQTrung (12/02/2022)
     */
    catchAxiosError(err) {
      console.log(err);
      this.errorMsg = resource.VI.errorMsg;
      this.isShowDialogErrMsg = true;
    },
  },
};
</script>
<style scoped src="../../assets/css/views/employee.css"></style>
