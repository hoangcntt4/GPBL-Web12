<template>
    <div class="m-menu">
            <div class="m-menu-logo">
                <a href="" class="toggle"></a>
                <a href="" class="logo"><img src="../../assets/img/Logo_Module_TiengViet_White.66947422.svg" alt="" height="24px"></a>
            </div>
            <div class="menu-item-list">
                <div class="menu-item">
                    <a href="" class="item-link">
                        <div class="menu-item-icon">
                            <div class="mi-icon-item mi-dashboard"></div>
                        </div>
                        <div class="menu-item-title">Tổng quan</div>
                    </a>
                </div>
                <div class="menu-item">
                    <a href="" class="item-link">
                        <div class="menu-item-icon">
                            <div class="mi-icon-item mi-cash"></div>
                        </div>
                        <div class="menu-item-title">Tiền mặt</div>
                    </a>
                </div>
                <div class="menu-item">
                    <a href="" class="item-link">
                        <div class="menu-item-icon">
                            <div class="mi-icon-item mi-bank"></div>
                        </div>
                        <div class="menu-item-title">Tiền gửi</div>
                    </a>
                </div>
                <div class="menu-item">
                    <a href="" class="item-link">
                        <div class="menu-item-icon">
                            <div class="mi-icon-item mi-pu"></div>
                        </div>
                        <div class="menu-item-title">Mua hàng</div>
                    </a>
                </div>
                <div class="menu-item">
                    <a href="" class="item-link">
                        <div class="menu-item-icon">
                            <div class="mi-icon-item mi-sale"></div>
                        </div>
                        <div class="menu-item-title">Bán hàng</div>
                    </a>
                </div>
                <div class="menu-item">
                    <a href="" class="item-link">
                        <div class="menu-item-icon">
                            <div class="mi-icon-item mi-invoice"></div>
                        </div>
                        <div class="menu-item-title">Quản lý hóa đơn</div>
                    </a>
                </div>
                <div class="menu-item">
                    <a href="" class="item-link">
                        <div class="menu-item-icon">
                            <div class="mi-icon-item mi-stock"></div>
                        </div>
                        <div class="menu-item-title">Kho</div>
                    </a>
                </div>
                <div class="menu-item">
                    <a href="" class="item-link">
                        <div class="menu-item-icon">
                            <div class="mi-icon-item mi-tools"></div>
                        </div>
                        <div class="menu-item-title">Công cụ dụng cụ</div>
                    </a>
                </div>
                <div class="menu-item">
                    <a href="" class="item-link">
                        <div class="menu-item-icon">
                            <div class="mi-icon-item mi-fixed-assets"></div>
                        </div>
                        <div class="menu-item-title">Tài sản cố định</div>
                    </a>
                </div>
                <div class="menu-item">
                    <a href="" class="item-link">
                        <div class="menu-item-icon">
                            <div class="mi-icon-item mi-tax"></div>
                        </div>
                        <div class="menu-item-title">Thuế</div>
                    </a>
                </div>
                <div class="menu-item">
                    <a href="" class="item-link">
                        <div class="menu-item-icon">
                            <div class="mi-icon-item mi-price"></div>
                        </div>
                        <div class="menu-item-title">Giá thành</div>
                    </a>
                </div>
                <div class="menu-item">
                    <a href="" class="item-link">
                        <div class="menu-item-icon">
                            <div class="mi-icon-item mi-general"></div>
                        </div>
                        <div class="menu-item-title">Tổng hợp</div>
                    </a>
                </div>
                <div class="menu-item">
                    <a href="" class="item-link">
                        <div class="menu-item-icon">
                            <div class="mi-icon-item mi-budget"></div>
                        </div>
                        <div class="menu-item-title">Ngân sách</div>
                    </a>
                </div>
                <div class="menu-item">
                    <a href="" class="item-link">
                        <div class="menu-item-icon">
                            <div class="mi-icon-item mi-report"></div>
                        </div>
                        <div class="menu-item-title">Báo cáo</div>
                    </a>
                </div>
                <div class="menu-item">
                    <a href="" class="item-link">
                        <div class="menu-item-icon">
                            <div class="mi-icon-item mi-finance"></div>
                        </div>
                        <div class="menu-item-title">Phân tích tài chính</div>
                    </a>
                </div>
            </div>
        </div>
</template>

<style scoped>
.m-menu{
    width: auto;
    background-color: #38393d;
}
.m-menu-logo{
    display: flex;
    padding: 24px;
    height: 48px;
    align-items: center;
    justify-content: center;
}
.toggle{
    width: 24px;
    height: 24px;
    margin-right: 10px;
    background: url('../../assets/img/Sprites.svg') no-repeat -424px -86px;
}
.logo{
    height: 32px;
    display: block;
}
.logo img{
    height: 32px;
}
.menu-item-list{
    height: calc(100vh - 48px);
    padding-top: 14px;
    overflow: auto;
}
.menu-item:hover{
    background-color: #bbb;
}
.item-link{
    height: 42px;
    line-height: 42px;
    display: flex;
    cursor: pointer;
    border-left: 6px solid transparent;
    padding-right: 14px;
    color: #fff;
    padding-left: 8px;
}

.menu-item-icon{
    margin-right: 8px;
    display: flex;
    align-items: center;
}

.mi-dashboard{
    background-position: -30px -1628px;
}
.mi-cash{
    background-position: -73px -1628px;
}
.mi-bank{
    background-position: -117px -1628px;
}
.mi-pu{
    background-position: -161px -1628px;
}
.mi-sale{
    background-position: -204px -1628px;
}
.mi-invoice{
    background-position: -249px -1628px;
}
.mi-stock{
    background-position: -291px -1628px;
}
.mi-tools{
    background-position: -336px -1628px;
}
.mi-fixed-assets{
    background-position: -378px -1630px;
}
.mi-tax{
    background-position: -423px -1630px;
}
.mi-price{
    background-position: -467px -1628px;
}
.mi-general{
    background-position: -510px -1628px;
}
.mi-budget{
    background-position: -378px -1659px;
}
.mi-report{
    background-position: -547px -1628px;
}
.mi-finance{
    background-position: -204px -1664px;
}
</style>