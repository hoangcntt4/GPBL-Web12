<template>
    <div id="lstEmployees">
        <div class="lstemployees-header">
            <div class="title-employee">Nhân viên</div>
            <div class="btnAddEmployee">
                <button class="m-button" @click="btnAddOnClick()">Thêm mới nhân viên</button>
            </div>
        </div>
        <div class="lstEmployees-content">
            <div class="employees-search">
                <div class="search">
                    <base-input/>
                </div>
                <div class="reload">
                    <div class="mi-icon-item mi-refresh"></div>
                </div>
            </div>
            <div class="employees-table">
                <table id="tbEmployees">
                    <thead>
                        <th style="width: 40px">
                            <input type="checkbox" style="width: 18px;height: 16px;cursor: pointer">
                        </th>
                        <th class="text-left" style="width: 134px !important">MÃ NHÂN VIÊN</th>
                        <th class="text-left">TÊN NHÂN VIÊN</th>
                        <th class="text-left" style="width: 105px">GIỚI TÍNH</th>
                        <th class="text-center" style="width: 150px">NGÀY SINH</th>
                        <th class="text-left" style="width: 120px">SỐ CMND</th>
                        <th class="text-left" style="width: 155px">CHỨC DANH</th>
                        <th class="text-left" style="width: 150px">TÊN ĐƠN VỊ</th>
                        <th class="text-center">CHỨC NĂNG</th>
                    </thead>
                    <tbody>
                        <tr v-for="employee in employees" :key="employee" @dblclick="dbClickonRow(employee.EmployeeId)">
                            <td class="text-center">
                                <input type="checkbox" style="width: 18px;height: 16px;cursor: pointer">
                            </td>
                            <td class="text-left">{{employee.EmployeeCode}}</td>
                            <td class="text-left">{{employee.EmployeeName}}</td>
                            <td class="text-left">{{FormatGender(employee.Gender)}}</td>
                            <td class="text-center">{{FormatDate(employee.DateOfBirth)}}</td>
                            <td class="text-left">{{employee.IdentityNumber}}</td>
                            <td class="text-left">{{employee.EmployeePosition}}</td>
                            <td class="text-left">{{employee.DepartmentName}}</td>
                            <td class="text-center">
                                <div class="fnc_btn">
                                    <div>
                                        <button class="btn_fix">Sửa</button>
                                        <button class="btn_arrow"><div class="mi-icon-item mi-arrow-up"></div></button>
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
                        <base-combobox/>
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
        <detail-employee :employeeId="employeeId" :editMode="editMode" />
    </div>
</template>
<script>
import BaseInput from '../../components/base/BaseInput.vue'
import BaseCombobox from '../../components/base/BaseCombobox.vue'
import DetailEmployee from '../../views/employees/DetailEmployee.vue'
import axios from "axios";
export default {
    name: "ListEmployees",
    components: { BaseInput, BaseCombobox, DetailEmployee},
    data:()=> ({
        employees: [],
        employeeId : null,
        editMode: null,
    }),
    async created(){
        this.GetData();
    },
    methods: {
        GetData(){
            var me = this;
            //Gọi api lấy dữ liệu list nhân viên
            axios.get("http://amis.manhnv.net/api/v1/Employees")
            .then(function(res){
                me.employees = res.data;
            }).catch(function(err){
                console.log(err);
            })
        },
        //Định dạng giới tính
        FormatGender(gender){
            if(gender==0){
                return "Nữ";
            }
            else if(gender==1){
                return "Nam";
            }
            else 
            return "Khác";
        },
        //Định dạng ngày sinh
        FormatDate(dob){
            const date = new Date(dob);
            const y = date.getFullYear().toString();
            const m = (date.getMonth() + 1).toString().padStart(2, "0");
            const d = date.getDate().toString().padStart(2, "0");
            return `${d}/${m}/${y}`
        },
        //Hiển thị form chi tiết 
        btnAddOnClick(){
            this.editMode = 1;
            document.getElementById("dialogEmployee").style.display = "block";
            this.employeeId = null;
        },
        //doubleClick hiển thị chi tiết nhân viên
        dbClickonRow(employeeId){
            this.editMode = 0;
            //gán employeeId trong data = employeeId từ row
            this.employeeId = employeeId;
            //hiển thị form chi tiết
            document.getElementById("dialogEmployee").style.display = "block";
        }
    },

}
</script>
<style scoped>
.lstemployees-header{
    display: flex;
    justify-content: space-between;
    padding: 16px 12px 12px 24px
}
.title-employee{
    font-size: 24px;
    font-weight: 700;
    color: #111;
}
.lstEmployees-content{
    margin: 0 24px;
    background-color: #fff;
    height: 640px;
    padding-bottom: 8px;
}
.btnAddEmployee{
    margin-right: 12px;
}
.employees-search{
    display: flex;
    justify-content: right;
    padding: 12px;
}
.search{
    padding-right: 12px;
}
.reload{
    height: 32px;
    width: 32px;
}
.mi-refresh{
    background-position: -423px -201px;
    margin: 4px;
}
/* table */
#tbEmployees{
    border-spacing: 0;
    min-width: 100%;
    overflow: scroll;
}
.employees-table{
    padding: 8px 24px 8px 16px;
    width: 100%;
    overflow: auto;
    height: calc(100% - 122px);
}
#tbEmployees thead th{
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
#tbEmployees tr:hover{
    background-color: #f3f8f8;
}
#tbEmployees td{
    height: 44px;
    border-bottom: 1px solid #c7c7c7;
    border-right: 1px dotted #c7c7c7;
    padding: 0 10px;
}

.mi-arrow-up{
    width: 16px;
    height: 16px;
    min-width: 16px;
    min-height: 16px;
    background-position: -896px -359px;
}
.fnc_btn div{
    display: flex;
    justify-content: center;
    text-align: center;
}
.btn_fix{
    color: #0075c0;
    font-weight: 600;
    width: 44px;
    height: 36px;
    padding: 6px 0 6px 16px;
}
.btn_fix:active{
    border: 1px solid #0075c0;
}
.btn_arrow{
    width: 46px;
    height: 36px;
    padding: 8px 10px;
}
.btn_arrow:active{
    border: 1px solid #0075c0;
}
/* pagination */
.ms-pagination{
    display: flex;
    justify-content: space-between;
    height: 56px;
    width: 85% !important;
    position: fixed;
    bottom: 10px;
    background-color: #fff;
    align-items: center;
}
.left-pagination{
    justify-items: center;
}
.right-pagination{
    display: flex;
    position: sticky;
    right: 80px;
    justify-content: center;
    align-items: center;
}
.total-record{
    padding-left: 24px;
}
.page-number{
    display: flex;
    cursor: pointer;
}
.page-index{
    display: flex;
}
.record-in-page{
    margin-right: 12px;
}
.page-index div{
    padding: 0 0.5rem;
}
.front-page{
    cursor: default!important;
    color: #9e9e9e;
}
</style>