<template>
  <div id="lstEmployees">
    <div class="lstemployees-header">
      <div class="title-employee">Nhân viên</div>
      <div class="btnAddEmployee">
        <button class="m-button" @click="btnAddOnClick()">
          Thêm mới nhân viên
        </button>
      </div>
    </div>
    <div class="lstEmployees-content">
      <div class="employees-search">
        <div class="search">
          <form action="" @submit.prevent="HandleSubmit">
            <div class="ms-input">
              <input
                class="m-input m-input-icon m-input-search"
                v-model="key_search"
                type="text"
                placeholder="Tìm theo mã, tên nhân viên"
              />
              <div class="mi-icon-item mi-search"></div>
            </div>
          </form>
        </div>
        <div class="reload">
          <div class="mi-icon-item mi-refresh" @click="GetData"></div>
        </div>
      </div>
      <div class="employees-table">
        <table id="tbEmployees">
          <thead>
            <th style="width: 40px">
              <input
                type="checkbox"
                style="width: 18px; height: 16px; cursor: pointer"
              />
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
              :key="employee"
              @dblclick="ShowFormDetail(employee.EmployeeId)"
            >
              <td class="text-center">
                <input
                  type="checkbox"
                  style="width: 18px; height: 16px; cursor: pointer"
                />
              </td>
              <td class="text-left">{{ employee.EmployeeCode }}</td>
              <td class="text-left">{{ employee.EmployeeName }}</td>
              <td class="text-left">{{ FormatGender(employee.Gender) }}</td>
              <td class="text-center">
                {{ FormatDate(employee.DateOfBirth) }}
              </td>
              <td class="text-left">{{ employee.IdentityNumber }}</td>
              <td class="text-left">{{ employee.EmployeePosition }}</td>
              <td class="text-left">{{ employee.DepartmentName }}</td>
              <td class="text-center">
                <div class="fnc_btn">
                  <div>
                    <button
                      class="btn_fix"
                      @click="ShowFormDetail(employee.EmployeeId)"
                    >
                      <div class="txtBtnFix">Sửa</div>
                    </button>
                    <button
                      class="btn_arrow"
                      @click="
                        btnDeleteClick(
                          employee.EmployeeId,
                          employee.EmployeeCode
                        )
                      "
                    >
                      <div class="mi-icon-item mi-arrow-up"></div>
                    </button>
                    <!-- <div class="contextMenu">
                                            <ul>
                                                <li>
                                                    Nhân bản
                                                </li>
                                                <li>
                                                    Xóa
                                                </li>
                                                <li>
                                                    Ngừng sử dụng
                                                </li>
                                            </ul>
                                        </div> -->
                  </div>
                </div>
              </td>
            </tr>
          </tbody>
          <tfoot></tfoot>
        </table>
      </div>
      <div class="ms-pagination">
        <div class="left-pagination">
          <div class="total-record">Tổng số: <b>64</b> bản ghi</div>
        </div>
        <div class="right-pagination">
          <div class="record-in-page">
            <base-combobox />
          </div>
          <div class="page-number">
            <div class="front-page">Trước</div>
            <div class="page-index">
              <div>1</div>
              <div>2</div>
              <div>3</div>
              <div>...</div>
              <div>52</div>
            </div>
            <div class="back-page">Sau</div>
          </div>
        </div>
      </div>
    </div>
    <detail-employee
      :employeeId="employeeId"
      :editMode="editMode"
      @LoadData="loadData"
    />
    <div class="dialog" id="dialog-delete">
      <div class="dialog-content">
        <div class="title-delete">
          <div class="mi-icon-item mi-warning"></div>
          <div class="title-confirm">
            <span style="display: block">
            Bạn có thực sự muốn xóa nhân viên &lt;{{ eCodeDelete }}>
            không?</span>
          </div>
        </div>
        <div class="hr-line"></div>
        <div class="btnConfirm">
          <button class="btnCancel" @click="btnCancleDel">Không</button>
          <button class="m-button" @click="btnDelete">Có</button>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
import BaseCombobox from "../../components/base/BaseCombobox.vue";
import DetailEmployee from "../../views/employees/DetailEmployee.vue";
import axios from "axios";
export default {
  name: "ListEmployees",
  components: { BaseCombobox, DetailEmployee },
  data: () => ({
    employees: [],
    employeeId: null,
    editMode: null,
    key_search: null,
    eIdDelete: null,
    eCodeDelete: null,
  }),
  async created() {
    this.GetData();
  },
  watch: {},
  methods: {
    //Check dữ liệu nhận từ component con: DetailEmployee
    loadData(checkLoad) {
      console.log(checkLoad);
      if (checkLoad == true) {
        this.GetData();
      }
    },
    GetData() {
      var me = this;
      //Gọi api lấy dữ liệu list nhân viên
      axios
        .get("http://amis.manhnv.net/api/v1/Employees")
        .then(function (res) {
          me.employees = res.data;
        })
        .catch(function (err) {
          console.log(err);
        });
    },

    //Định dạng giới tính
    FormatGender(gender) {
      if (gender == 0) {
        return "Nữ";
      } else if (gender == 1) {
        return "Nam";
      } else return "Khác";
    },
    //Định dạng ngày sinh
    FormatDate(dob) {
      const date = new Date(dob);
      const y = date.getFullYear().toString();
      const m = (date.getMonth() + 1).toString().padStart(2, "0");
      const d = date.getDate().toString().padStart(2, "0");
      return `${d}/${m}/${y}`;
    },

    //Hiển thị form chi tiết
    btnAddOnClick() {
      this.editMode = 1;
      document.getElementById("dialogEmployee").style.display = "block";
      this.employeeId = null;
    },

    //doubleClick hiển thị chi tiết nhân viên
    ShowFormDetail(employeeId) {
      this.editMode = 0;
      //gán employeeId trong data = employeeId từ row
      this.employeeId = employeeId;
      //hiển thị form chi tiết
      document.getElementById("dialogEmployee").style.display = "block";
    },

    //Xóa nhân viên
    btnCancleDel() {
      document.getElementById("dialog-delete").style.display = "none";
      this.eIdDelete = null;
      this.eCodeDelete = null;
    },
    btnDeleteClick(eId, eCode) {
      document.getElementById("dialog-delete").style.display = "block";
      this.eIdDelete = eId;
      this.eCodeDelete = eCode;
    },
    btnDelete() {
      var me = this;
      //gọi API xóa dữ nhân viên
      axios
        .delete(`http://amis.manhnv.net/api/v1/Employees/${this.eIdDelete}`)
        .then(function (res) {
          console.log(res);
          document.getElementById("dialog-delete").style.display = "none";
          me.GetData();
        })
        .catch(function (err) {
          console.log(err);
        });
    },
    //Tìm kiếm theo tên nhân viên
    HandleSubmit() {
      var me = this;
      axios
        .get(
          `http://amis.manhnv.net/api/v1/Employees/filter?employeeFilter=${this.key_search}`
        )
        .then(function (res) {
          console.log(res.data);
          me.employees = res.data;
          console.log(me.employees);
        })
        .catch(function (err) {
          console.log(err);
        });
    },
  },
};
</script>
<style scoped>
.lstemployees-header {
  display: flex;
  justify-content: space-between;
  padding: 16px 12px 12px 24px;
}
.title-employee {
  font-size: 24px;
  font-weight: 700;
  color: #111;
}
.lstEmployees-content {
  margin: 0 24px;
  background-color: #fff;
  height: 640px;
  padding-bottom: 8px;
}
.btnAddEmployee {
  margin-right: 12px;
}
.employees-search {
  display: flex;
  justify-content: right;
  padding: 12px;
}
.search {
  padding-right: 12px;
}
.reload {
  height: 32px;
  width: 32px;
}
.mi-refresh {
  background-position: -423px -201px;
  margin: 4px;
}
/* table */
#tbEmployees {
  border-spacing: 0;
  min-width: 100%;
  overflow: scroll;
}
.employees-table {
  padding: 8px 24px 8px 16px;
  width: 100%;
  overflow: auto;
  height: calc(100% - 122px);
}
#tbEmployees thead th {
  height: 34px;
  font-size: 12px;
  width: 143px;
  background-color: #eceef1;
  vertical-align: middle;
  position: sticky;
  border-right: 1px solid #c7c7c7;
  border-bottom: 1px solid #c7c7c7;
  padding: 0 10px;
}
#tbEmployees tr:hover {
  cursor: pointer;
  background-color: #f3f8f8;
}
#tbEmployees td {
  height: 44px;
  border-bottom: 1px solid #c7c7c7;
  border-right: 1px dotted #c7c7c7;
  padding: 0 10px;
}

.mi-arrow-up {
  width: 16px;
  height: 16px;
  min-width: 16px;
  min-height: 16px;
  background-position: -896px -359px;
}
.fnc_btn div {
  display: flex;
  justify-content: center;
  text-align: center;
}
.btn_fix {
  color: #0075c0;
  font-weight: 600;
  width: 44px;
  height: 36px;
  padding: 6px 0 6px 16px;
}
.btn_fix:active .txtBtnFix {
  border: 1px solid #0075c0;
}
.btn_arrow {
  width: 46px;
  height: 36px;
  padding: 8px 10px;
}
.btn_arrow:active .mi-arrow-up {
  border: 1px solid #0075c0;
}
/* pagination */
.ms-pagination {
  display: flex;
  justify-content: space-between;
  height: 56px;
  width: 85% !important;
  position: fixed;
  bottom: 10px;
  background-color: #fff;
  align-items: center;
}
.left-pagination {
  justify-items: center;
}
.right-pagination {
  display: flex;
  position: sticky;
  right: 80px;
  justify-content: center;
  align-items: center;
}
.total-record {
  padding-left: 24px;
}
.page-number {
  display: flex;
  cursor: pointer;
}
.page-index {
  display: flex;
}
.record-in-page {
  margin-right: 12px;
}
.page-index div {
  padding: 0 0.5rem;
}
.front-page {
  cursor: default !important;
  color: #9e9e9e;
}

/* context-Menu */
.contextMenu {
  position: absolute;
  width: 200px;
  top: 0;
}

/* popup xóa  */
#dialog-delete .dialog-content {
  padding: 32px;
}
.title-delete {
  height: 80px;
  display: flex;
  width: 380px;
}
.mi-warning {
  background-position: -592px -456px;
  width: 48px;
  height: 48px;
  min-width: 48px;
  min-height: 48px;
}
.title-confirm{
  overflow: auto;
    max-height: 400px;
    margin-bottom: 32px;
    padding-left: 16px;
    padding-top: 12px;
}
.hr-line {
  height: 1px;
  background: #b8bcc3;
  margin-bottom: 20px;
}
.btnConfirm {
  height: 32px;
  display: flex;
  justify-content: space-between;
}
.btnConfirm button {
  height: 32px !important;
}
.btnCancel {
  border: 1px solid #8d9096;
  color: #111;
  background-color: transparent;
  border-radius: 4px;
  padding: 6px 16px;
}
</style>
