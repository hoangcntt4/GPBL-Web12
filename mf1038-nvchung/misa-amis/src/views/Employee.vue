<template>
  <layout>
    <div class="title">
      <h4>Nhân viên</h4>
      <m-button @click="handleAdd">Thêm mới nhân viên</m-button>
    </div>
    <div class="main-content">
      <div class="flex justify-between mb-16">
        <m-dropdown style="z-index: 103" :contentFull="false">
          <template #trigger>
            <m-button
              @click="handleOpenBulkActions"
              pill
              outlined
              color="secondary"
              class="flex items-center"
              >Thực hiện hàng loạt &nbsp;<m-icon
                icon="arrow-down-black"
                :size="16"
            /></m-button>
          </template>
          <m-list>
            <m-list-item @click="handleBulkDelete">Xóa</m-list-item>
          </m-list>
        </m-dropdown>
        <div class="flex items-center gap-10">
          <m-input
            v-model="searchText"
            placeholder="Tìm theo mã, tên nhân viên"
            icon="search"
          />
          <m-icon icon="refresh" @click="handleResetData" />
          <m-icon icon="excel" @click="handleExportExcel" />
          <m-icon icon="settings" @click="showCustomizerPopup = true" />
        </div>
      </div>
      <m-table
        :headers="selectedEmployeeHeaders"
        :items="employees"
        keyName="EmployeeId"
        style="max-height: 440px"
        v-model="selectedEmployeeIds"
      >
        <template #actions="{ item }">
          <m-button
            variant="text"
            color="blue"
            size="sm"
            @click="editEmp(item.EmployeeId)"
            >Sửa</m-button
          >
          <m-dropdown appendToBody>
            <template #trigger="{ active }">
              <m-button variant="icon" size="sm">
                <m-icon
                  icon="arrow-down-blue"
                  :size="16"
                  :style="active && 'border: 1px solid #0075c2;'"
                />
              </m-button>
            </template>
            <m-list>
              <m-list-item @click="clone(item.EmployeeId)"
                >Nhân bản</m-list-item
              >
              <m-list-item @click="delEmp(item)">Xóa</m-list-item>
              <m-list-item>Ngừng sử dụng</m-list-item>
            </m-list>
          </m-dropdown>
        </template>
      </m-table>
      <div
        class="flex flex-col items-center mt-16"
        v-if="employees.length == 0"
      >
        <img src="../assets/imgs/no-content.svg" alt="no-content" />
        <p class="mt-16">Không có dữ liệu</p>
      </div>
      <div class="paging">
        <div>
          Tổng số: <b>{{ employeeCount }}</b> bản ghi
        </div>
        <div class="flex items-center">
          <m-combo-box
            :items="pages"
            labelMember="text"
            readonly
            :initialSelected="pages[0]"
            @select="handlePerPageChange"
            top
          />
          <m-pagination
            :itemCount="employeeCount"
            :perPage="perPage"
            :page="page"
            @pageChange="handlePageChange"
          />
        </div>
      </div>
    </div>
  </layout>
  <m-spinner v-if="appLoading" />
  <employee-popup
    @close="handlePopupClose"
    v-if="showPopup"
    :employeeId="currentEmployeeId"
    :mode="popupMode"
    @saveAndAdd="handleSaveAndAdd"
  />
  <m-message-dialog v-bind="messageProps" v-on="messageEvents" />
  <m-popup @close="showCustomizerPopup = false" v-if="showCustomizerPopup">
    <template #title>
      <h2>Tùy chỉnh giao diện</h2>
    </template>
    <m-table
      :headers="CUSTOMIZER_TABLE_HEADERS"
      :items="EMPLOYEE_HEADERS"
      keyName="propName"
      v-model="selectedEmployeePropNames"
      style="max-height: 440px"
    />
    <template #footer>
      <m-button color="secondary" @click="showCustomizerPopup = false"
        >Hủy</m-button
      >
      <div>
        <m-button color="secondary" @click="handleResetCustomizer"
          >Lấy mẫu ngầm định</m-button
        >&nbsp;
        <m-button @click="handleSaveCustomizer">Cất</m-button>
      </div>
    </template>
  </m-popup>
</template>
<script>
import Layout from "../components/layouts/Layout.vue";
import MButton from "../components/bases/MButton.vue";
import MInput from "../components/bases/MInput.vue";
import MIcon from "../components/bases/MIcon.vue";
import MTable from "../components/bases/MTable.vue";
import MDropdown from "../components/bases/MDropdown.vue";
import MList from "../components/bases/List/MList.vue";
import MListItem from "../components/bases/List/MListItem.vue";
import MComboBox from "../components/bases/MCombobox.vue";
import MPagination from "../components/bases/MPagination.vue";
import MSpinner from "../components/bases/MSpinner.vue";
import EmployeePopup from "./EmployeePopup.vue";
import MMessageDialog from "../components/bases/MMessageDialog.vue";
import MPopup from "../components/bases/MPopup.vue";
import ApiConfig from "../api-config";
import useMessageDialog from "../composables/useMessageDialog";
export default {
  components: {
    Layout,
    MButton,
    MInput,
    MTable,
    MDropdown,
    MIcon,
    MList,
    MListItem,
    MComboBox,
    MPagination,
    MSpinner,
    EmployeePopup,
    MMessageDialog,
    MPopup,
  },
  setup() {
    // tat ca header cua table employee
    const EMPLOYEE_HEADERS = [
      { text: "Mã nhân viên", propName: "EmployeeCode", width: 120 },
      { text: "Tên nhân viên", propName: "FullName", width: 174 },
      {
        text: "Giới tính",
        propName: "Gender",
        width: 80,
        format: "gender",
      },
      {
        text: "Ngày sinh",
        propName: "DateOfBirth",
        format: "date",
        width: 110,
      },
      { text: "Chức danh", propName: "EmployeePosition", width: 150 },
      { text: "Số CMND", propName: "IdentityNumber", width: 150 },
      {
        text: "Ngày cấp",
        propName: "IdentityDate",
        width: 110,
        format: "date",
      },
      { text: "Nơi cấp", propName: "IdentityPlace", width: 140 },
      { text: "Số tài khoản", propName: "BankAccountNumber", width: 150 },
      { text: "Tên ngân hàng", propName: "BankName", width: 200 },
      {
        text: "Chi nhánh TK ngân hàng",
        propName: "BankBranchName",
        width: 200,
      },
      { text: "Địa chỉ", propName: "Address", width: 200 },
      { text: "ĐT di động", propName: "PhoneNumber", width: 150 },
      { text: "ĐT cố định", propName: "TelephoneNumber", width: 150 },
      { text: "Email", propName: "Email", width: 200 },
      { text: "Mã đơn vị", propName: "DepartmentCode", width: 150 },
      { text: "Tên đơn vị", propName: "DepartmentName", width: 200 },
      { text: "Ngày tạo", propName: "CreatedDate", format: "date", width: 150 },
      { text: "Người tạo", propName: "CreatedBy", width: 150 },
      {
        text: "Ngày sửa",
        propName: "ModifiedDate",
        format: "date",
        width: 150,
      },
      { text: "Người sửa", propName: "ModifiedBy", width: 150 },
    ];
    //header cua table tuy chinh giao dien
    const CUSTOMIZER_TABLE_HEADERS = [
      { text: "Tên cột dữ liệu", propName: "text" },
    ];
    //header mac dinh cua table employee
    const DEFAULT_EMPLOYEE_HEADERS = EMPLOYEE_HEADERS.slice(0, 8);
    return {
      EMPLOYEE_HEADERS,
      CUSTOMIZER_TABLE_HEADERS,
      DEFAULT_EMPLOYEE_HEADERS,
      ...useMessageDialog(),
    };
  },
  created() {
    //load du lieu len table
    this.loadEmployees();
  },
  methods: {
    handleAdd() {
      this.currentEmployeeId = undefined;
      this.popupMode = "add";
      this.showPopup = true;
    },
    handleBulkDelete() {
      this.showConfirm({
        text: "Bạn có thực sự muốn xóa những nhân viên đã chọn không?",
        icon: "warning",
        onYes: async () => {
          this.appLoading = true;
          try {
            await this.$axios.post(
              ApiConfig.Employee.BULK,
              this.selectedEmployeeIds
            );
            await this.loadEmployees();
          } catch {
            this.showError({
              text: "Có lỗi xảy ra. Vui lòng liên hệ Misa để được hỗ trợ.",
            });
          } finally {
            this.appLoading = false;
          }
        },
      });
    },
    handleExportExcel() {
      location.href = ApiConfig.Employee.EXCEL_FILE;
    },
    handleOpenBulkActions(evt) {
      if (this.selectedEmployeeIds.length == 0) {
        evt.stopPropagation();
      }
    },
    handleResetData() {
      this.searchText = "";
      this.page = 1;
      this.perPage = 10;
    },
    //reset tuy chinh giao dien ve mac dinh
    handleResetCustomizer() {
      this.selectedEmployeePropNames = this.DEFAULT_EMPLOYEE_HEADERS.map(
        ({ propName }) => propName
      );
    },
    //luu lai tuy chinh giao dien
    handleSaveCustomizer() {
      this.selectedEmployeeHeaders = this.EMPLOYEE_HEADERS.filter(
        ({ propName }) => this.selectedEmployeePropNames.includes(propName)
      ); //loc ra cac header duoc nguoi dung chon
      this.showCustomizerPopup = false; //dong popup tuy chinh
    },
    //dong popup thong tin nhan vien
    handlePopupClose(status) {
      this.showPopup = false;
      if (status == 1) {
        this.loadEmployees(); //neu status tra ve tu popup=1 reload table
      }
    },
    //bat su kien cat va them
    handleSaveAndAdd() {
      this.currentEmployeeId = undefined;
      this.popupMode = "add";
      this.loadEmployees();
    },
    /**
     * @param {String} search - search keyword
     * ham load employee co phan trang va loc du lieu
     */
    async loadEmployees() {
      this.appLoading = true; //bat spinner
      try {
        const { data } = await this.$axios.get(ApiConfig.Employee.FILTER, {
          params: {
            pageSize: this.perPage, //so ban ghi tren trang
            pageNumber: this.page, //trang hien tai
            search: this.searchText, //search keyword
          },
        });
        this.employees = data.Data;
        this.pageCount = data.TotalPages; //tong so trang
        this.employeeCount = data.TotalRecords; //tong so record
      } catch {
        this.showError({
          text: "Có lỗi xảy ra. Vui lòng liên hệ Misa để được hỗ trợ.",
        });
      } finally {
        this.appLoading = false; //tat spinner
      }
    },
    //ham sua employee
    editEmp(empId) {
      this.popupMode = "edit";
      this.currentEmployeeId = empId; //gan id cua employee duoc chon de sua
      this.showPopup = true; //show popup chinh sua
    },
    clone(empId) {
      this.popupMode = "clone";
      this.currentEmployeeId = empId;
      this.showPopup = true;
    },
    //ham xoa employee
    async delEmp(emp) {
      this.showConfirm({
        text: `Bạn có thực sự muốn xóa nhân viên ${emp.EmployeeCode} không?`,
        icon: "warning",
        onYes: async () => {
          this.appLoading = true;
          try {
            //thuc hien xoa va load lai du lieu
            await this.$axios.delete(
              `${ApiConfig.Employee.BASE}/${emp.EmployeeId}`
            );
            this.loadEmployees();
          } catch {
            this.showError({
              text: "Có lỗi xảy ra. Vui lòng liên hệ Misa để được hỗ trợ.",
            });
          } finally {
            this.appLoading = false;
          }
        },
      });
    },
    //neu so ban ghi tren trang thay doi
    handlePerPageChange(perPage) {
      this.perPage = perPage.value;
      this.loadEmployees();
    },
    //neu trang hien tai thay doi
    handlePageChange(page) {
      this.page = page;
      this.loadEmployees();
    },
  },
  data() {
    return {
      searchText: "",
      selectedEmployeeHeaders: this.DEFAULT_EMPLOYEE_HEADERS, //cac header duoc chon cua bang employee
      pages: [
        { text: "10 bản ghi trên 1 trang", value: 10 },
        { text: "20 bản ghi trên 1 trang", value: 20 },
        { text: "30 bản ghi trên 1 trang", value: 30 },
        { text: "50 bản ghi trên 1 trang", value: 50 },
        { text: "100 bản ghi trên 1 trang", value: 100 },
      ], //cac lua chon cua combobox ban ghi /trang
      employees: [], //du lieu cua table
      currentEmployeeId: undefined, //id duoc chon de sua
      popupMode: "add",
      perPage: 10, //so ban ghi /trang
      page: 1, //trang hien tai
      pageCount: 0, //tong so trang
      employeeCount: 0, //tong so ban ghi
      showPopup: false, //co hien popup tt nv khong
      appLoading: false, //co hien spinner ko
      showCustomizerPopup: false, //co hien popup tuy chinh ko
      selectedEmployeePropNames: this.DEFAULT_EMPLOYEE_HEADERS.map(
        ({ propName }) => propName
      ), //cac thuoc tinh duoc chon tren tuy chinh popup
      selectedEmployeeIds: [], //cac id eployee duoc chon
    };
  },
  watch: {
    searchText() {
      this.page = 1;
      this.loadEmployees();
    },
  },
};
</script>
<style scoped src="../styles/views/employee.css"></style>
