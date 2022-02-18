<template>
    <div class="m-menu">
            <div class="m-menu-logo">
                <a href="" class="toggle"></a>
                <router-link to="/" class="logo"><img src="../../assets/img/Logo_Module_TiengViet_White.66947422.svg" alt="" height="24px"></router-link>
            </div>
            <div class="menu-item-list">
                <div class="menu-item">
                    <router-link to="/content" class="item-link">
                        <div class="menu-item-icon">
                            <base-icon icon="dashboard"></base-icon>
                        </div>
                        <div class="menu-item-title">Tổng quan</div>
                    </router-link>
                </div>
                <div class="menu-item">
                    <router-link to="/listEmployee" class="item-link">
                        <div class="menu-item-icon">
                            <base-icon icon="cash"></base-icon>
                        </div>
                        <div class="menu-item-title">Tiền mặt</div>
                    </router-link>
                </div>
                <div class="menu-item">
                    <a href="" class="item-link">
                        <div class="menu-item-icon">
                            <base-icon icon="bank"></base-icon>
                        </div>
                        <div class="menu-item-title">Tiền gửi</div>
                    </a>
                </div>
                <div class="menu-item">
                    <a href="" class="item-link">
                        <div class="menu-item-icon">
                            <base-icon icon="pu"></base-icon>
                        </div>
                        <div class="menu-item-title">Mua hàng</div>
                    </a>
                </div>
                <div class="menu-item">
                    <a href="" class="item-link">
                        <div class="menu-item-icon">
                            <base-icon icon="sale"></base-icon>
                        </div>
                        <div class="menu-item-title">Bán hàng</div>
                    </a>
                </div>
                <div class="menu-item">
                    <a href="" class="item-link">
                        <div class="menu-item-icon">
                            <base-icon icon="invoice"></base-icon>
                        </div>
                        <div class="menu-item-title">Quản lý hóa đơn</div>
                    </a>
                </div>
                <div class="menu-item">
                    <a href="" class="item-link">
                        <div class="menu-item-icon">
                            <base-icon icon="stock"></base-icon>
                        </div>
                        <div class="menu-item-title">Kho</div>
                    </a>
                </div>
                <div class="menu-item">
                    <a href="" class="item-link">
                        <div class="menu-item-icon">
                            <base-icon icon="tools"></base-icon>
                        </div>
                        <div class="menu-item-title">Công cụ dụng cụ</div>
                    </a>
                </div>
                <div class="menu-item">
                    <a href="" class="item-link">
                        <div class="menu-item-icon">
                            <base-icon icon="fixed-assets"></base-icon>
                        </div>
                        <div class="menu-item-title">Tài sản cố định</div>
                    </a>
                </div>
                <div class="menu-item">
                    <a href="" class="item-link">
                        <div class="menu-item-icon">
                            <base-icon icon="tax"></base-icon>
                        </div>
                        <div class="menu-item-title">Thuế</div>
                    </a>
                </div>
                <div class="menu-item">
                    <a href="" class="item-link">
                        <div class="menu-item-icon">
                            <base-icon icon="price"></base-icon>
                        </div>
                        <div class="menu-item-title">Giá thành</div>
                    </a>
                </div>
                <div class="menu-item">
                    <a href="" class="item-link">
                        <div class="menu-item-icon">
                            <base-icon icon="general"></base-icon>
                        </div>
                        <div class="menu-item-title">Tổng hợp</div>
                    </a>
                </div>
                <div class="menu-item">
                    <a href="" class="item-link">
                        <div class="menu-item-icon">
                            <base-icon icon="budget"></base-icon>
                        </div>
                        <div class="menu-item-title">Ngân sách</div>
                    </a>
                </div>
                <div class="menu-item">
                    <a href="" class="item-link">
                        <div class="menu-item-icon">
                            <base-icon icon="report"></base-icon>
                        </div>
                        <div class="menu-item-title">Báo cáo</div>
                    </a>
                </div>
                <div class="menu-item">
                    <a href="" class="item-link">
                        <div class="menu-item-icon">
                            <base-icon icon="finance"></base-icon>
                        </div>
                        <div class="menu-item-title">Phân tích tài chính</div>
                    </a>
                </div>
            </div>
        </div>
</template>
<script>
import BaseIcon from '../base/BaseIcon.vue';
export default {
  components: { BaseIcon },
    
}
</script>
<style scoped src="../../assets/css/components/theNavbar.css">
</style>