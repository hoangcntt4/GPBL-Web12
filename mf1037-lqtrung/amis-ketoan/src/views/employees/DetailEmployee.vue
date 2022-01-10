<template>
    <div class="dialog" id="dialogEmployee">
        <div class="dialog-content">
            <div class="popup-header">
                <div class="popup-title">
                    <div class="popup-title-container">
                        <div class="title__popup">
                            Thông tin nhân viên
                        </div>
                        <div class="cbo">
                            <div class="checkbox">
                                <input type="checkbox"> 
                                <div>
                                    Là khách hàng
                                </div>
                            </div>
                            <div class="checkbox">
                                <input type="checkbox"> 
                                <div>
                                    Là nhà cung cấp
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="popup-support">
                    <div class="mi-icon-item mi-help"></div>
                    <div class="mi-icon-item mi-close" @click="btnCancelOnClick"></div>
                </div>
            </div>
            <div class="popup-content-container">
                <div class="popup-content">
                    <div class="pop-top">
                        <div class="e-left">
                            <div class="top-e-ip-1">
                                <div class="top-e-ip-1-left"  style="padding-right: 6px">
                                    <div class="ip-text">
                                        <span>Mã</span>
                                    </div>
                                    <div>
                                        <input type="text" class="m-input" style="width: 150px" v-model="employee.EmployeeCode">
                                    </div>
                                </div>
                                <div class="top-e-ip-1-right">
                                    <div class="ip-text">
                                        <span>Tên</span>
                                    </div>
                                    <div>
                                        <input type="text" class="m-input" style="width: 235px" v-model="employee.EmployeeName">
                                    </div>
                                </div>
                            </div>
                            <div class="top-e-ip-2">
                                <div class="ip-text">
                                    <span>Đơn vị</span>
                                </div>
                                <div>
                                     <input type="text" class="m-input" style="width: 391px" v-model="employee.DepartmentId">
                                </div>
                            </div>
                            <div class="top-e-ip-3">
                                <div class="ip-text">
                                    <span>Chức danh</span>
                                </div>
                                <div>
                                     <input type="text" class="m-input" style="width: 391px" v-model="employee.EmployeePosition">
                                </div>
                            </div>
                        </div>
                        <div class="e-right">
                            <div class="top-e-ip-1">
                                <div class="top-e-ip-1-left"  style="padding-right: 6px">
                                    <div class="ip-text">
                                        <span>Ngày sinh</span>
                                    </div>
                                    <div>
                                        <input type="date" class="m-input" style="width: 161px" v-model="employee.DateOfBirth">
                                    </div>
                                </div>
                                <div class="top-e-ip-1-right" style="padding-left: 10px">
                                    <div class="ip-text">
                                        <span>Giới tính</span>
                                    </div>
                                    <div class="gender-radio">
                                        <div style="padding-right: 20px">
                                            <input class="m-radio" type="radio" id="Nam" value="1" v-model="employee.Gender">
                                            <label for="Nam">Nam</label>
                                        </div>
                                        <div>
                                            <input class="m-radio" type="radio" id="Nu" value="0" v-model="employee.Gender">
                                            <label label for="Nu">Nữ</label>
                                        </div>
                                        <div>
                                            <input class="m-radio" type="radio" id="Khac" :value="null" v-model="employee.Gender">
                                            <label label for="Khac">Khác</label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="top-e-ip-2" style="display: flex">
                                <div  style="padding-right: 6px">
                                    <div class="ip-text">
                                        <span>Số CMND</span>
                                    </div>
                                    <div>
                                        <input type="text" class="m-input" style="width: 245px" v-model="employee.IdentityNumber">
                                    </div>
                                </div>
                                <div>
                                    <div class="ip-text">
                                        <span>Ngày cấp</span>
                                    </div>
                                    <div>
                                        <input type="date" class="m-input" style="width: 168px" v-model="employee.IdentityDate">
                                    </div>
                                </div>
                            </div>
                            <div class="top-e-ip-3">
                                <div class="ip-text">
                                    <span>Nơi cấp</span>
                                </div>
                                <div>
                                     <input type="text" class="m-input" style="width: 419px" v-model="employee.IdentityPlace">
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="pop-bot">
                        <div class="pop-bot-info">
                            <div class="info-container">
                                <div class="info-detail">
                                    <div>
                                        <div class="ip-text">
                                            <span>Địa chỉ</span>
                                        </div>
                                        <div>
                                            <input type="text" class="m-input" style="width: 100%" v-model="employee.Address">
                                        </div>
                                    </div>
                                    <div class="info-contact">
                                        <div>
                                            <div class="ip-text">
                                                <span>ĐT di động</span>
                                            </div>
                                            <div>
                                                <input type="text" class="m-input" style="width: 197px" v-model="employee.TelephoneNumber">
                                            </div>
                                        </div>
                                        <div>
                                            <div class="ip-text">
                                                <span>ĐT cố định</span>
                                            </div>
                                            <div>
                                                <input type="text" class="m-input" style="width: 197px" v-model="employee.PhoneNumber">
                                            </div>
                                        </div>
                                        <div>
                                            <div class="ip-text">
                                                <span>Email</span>
                                            </div>
                                            <div>
                                                <input type="text" class="m-input" style="width: 197px" v-model="employee.Email">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="info-contact">
                                        <div>
                                            <div class="ip-text">
                                                <span>Tài khoản ngân hàng</span>
                                            </div>
                                            <div>
                                                <input type="text" class="m-input" style="width: 197px" v-model="employee.BankAccountNumber">
                                            </div>
                                        </div>
                                        <div>
                                            <div class="ip-text">
                                                <span>Tên ngân hàng</span>
                                            </div>
                                            <div>
                                                <input type="text" class="m-input" style="width: 197px" v-model="employee.BankName">
                                            </div>
                                        </div>
                                        <div>
                                            <div class="ip-text">
                                                <span>Chi nhánh</span>
                                            </div>
                                            <div>
                                                <input type="text" class="m-input" style="width: 197px" v-model="employee.BankBranchName">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="divide"></div>
                <div class="popup-footer-container">
                    <div class="popup-footer">
                        <div>
                            <button class="m-button btn-default" @click="btnCancelOnClick">Hủy</button>
                        </div>
                        <div class="footer-btn-right">
                            <div style="padding: 0 10px">
                                <button class="m-button btn-default">Cất</button>
                            </div>
                            <div>
                                <button class="m-button" @click="btnSaveOnClick">Cất và Thêm</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            
        </div>
    </div>
</template>

<script>
import axios from 'axios';
export default {
    name: "DetailEmployee",
    props: ["employeeId","editMode"],
    data:()=> ({
        employee: {},
    }),
    watch:{
        employeeId: function(value){
            var me = this;
            if(value){
                axios.get(`http://amis.manhnv.net/api/v1/Employees/${value}`)
                .then(function(res){
                    console.log(res);
                    me.employee = res.data;
                    me.employee.DateOfBirth = me.FormatDate(res.data.DateOfBirth);
                    me.employee.IdentityDate = me.FormatDate(res.data.IdentityDate)
                }).catch(function(err){
                    console.log(err);
                })
            }
            else{
                this.employee = {};
            }
        }
    },
    methods:{
        /* Lưu dữ liệu
        Author: LQTrung(7/1/2022) */
        btnSaveOnClick(){
            switch (this.editMode) {
                case 1:
                    //gọi API thực hiện thêm nhân viên
                    axios.post('http://amis.manhnv.net/api/v1/Employees', this.employee)
                    .then(function(res){
                        console.log(res);
                        document.getElementById("dialogEmployee").style.display = "none";
                    }).catch(function(err){
                        console.log(err);
                    });
                    break;
                case 0:
                    //gọi API thực hiện sửa nhân viên
                    axios.put(`http://amis.manhnv.net/api/v1/Employees/${this.employee.EmployeeId}`,this.employee)
                    .then(function(res){
                        console.log(res);
                        document.getElementById("dialogEmployee").style.display = "none";
                    }).catch(function(err){
                        console.log(err);
                    })
                    break;
                default:
                    break;
            }
        },
        //Ẩn form chi tiết
        btnCancelOnClick(){
            document.getElementById("dialogEmployee").style.display = "none";
            this.employee = null;
            console.log(this.editMode)
        },
        //định dạng ngày tháng
        FormatDate(dob){
            const date = new Date(dob);
            const y = date.getFullYear().toString();
            const m = (date.getMonth() + 1).toString().padStart(2, "0");
            const d = date.getDate().toString().padStart(2, "0");
            return `${y}-${m}-${d}`
        },
    }
}
</script>

<style scoped>
.dialog{
    display: none;
    position: fixed;
    top: 0;
    bottom: 0;
    left: 0;
    right: 0;
    z-index: 1;
    background-color: rgba(0, 0, 0, 0.4);
}

.dialog .dialog-content{
    width: 900px;
    background-color: #fff;
    top: 50%;
    left: 50%;
    transform: translate(-50%, -50%);
    position: absolute;
}
/* popup-header */
.popup-header{
    display: flex;
    justify-content: space-between;
}
/* popup-title */
.popup-title{
    width: 100%;
    padding: 20px 32px;
    padding-right: 12px;
    position: relative;
    overflow: hidden;
    border-radius: 5px 5px 0 0;
}

.popup-title .popup-title-container{
    display: flex;
}
.title__popup{
    font-size: 24px;
    color: #111;
    font-weight: 700;
}
.cbo{
    display: flex;
    align-items: center;
}
.cbo input{
    height: 20px;
    width: 24px;
}
.cbo .checkbox{
    display: flex;
    padding: 0 20px;
}
.cbo .checkbox div{
    padding-left: 10px;
}


/* support */
.popup-support{
    display: flex;
    padding: 12px;
}
.popup-support div:first-child{
    margin-right: 6px;
}
.mi-close{
    background-position: -144px -144px;
}
.mi-help{
    background-position: -89px -144px;
}

/* popup-content */
.popup-content-container{
    flex-direction: column;
    display: flex;
    overflow: auto;
    overflow-y: visible;
    padding: 0 32px 20px;
    margin-bottom: 16px;
}
.top-e-ip-1{
    display: flex;
}
.e-left{
    padding-right: 26px;
}
.e-left > div{
    padding: 0 0 12px;
}
.e-right > div{
    padding: 0 0 12px;
}
.pop-top{
    display: flex;
}
.gender-radio{
    display: flex;
}
.ip-text{
    padding-bottom: 3px;
}
.ip-text span{
    font-weight: 600;
    font-size: 12px;
}



/* pop-bot-info */
.pop-bot-info{
    height: 194px;
    width: 836px;
}
.info-detail{
    margin: 24px 0;
}
.info-contact{
    display: flex;
    padding-top: 12px;
}
.info-contact > div{
    padding-right: 6px;
}

.divide{
    width: 100%;
    border-top: 1px solid #e0e0e0;
    margin: 32px 0 20px;
}

/* popup-footer */
.popup-footer{
    display: flex;
    justify-content: space-between;
    height: 34px;
}
.footer-btn-right{
    display: flex;
}
.btn-default{
    border: 1px solid #8d9096;
    color: #111;
    background-color: transparent;
    padding: 6px 16px;
}
.btn-default:hover{
    background-color: #d2d3d6;
}
.btn-default:active{
    background-color: #bbbcbc;
}
</style>