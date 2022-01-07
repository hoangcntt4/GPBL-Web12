<template>
    <div class="navbar-custom">
        <div class="left">
            <div class="mi-icon-item mi-stripes"></div>
            <div class="header-branch">
                <div class="header-branch-name">CÔNG TY TNHH SẢN XUẤT - THƯƠNG MẠI - DỊCH VỤ QUI PHÚC</div>
                <div class="mi-icon-item mi-chevron"></div>
            </div>
        </div>
        <div class="right">
            <div class="mi-icon-item mi-bell"></div>
            <div class="user-dropdown">
                <div class="user-avatar">
                    <div class="mi-icon-item mi-avatar"></div>
                </div>
                <div>
                    <div class="user-name">Lê Quang Trung</div>
                </div>
                <div class="mi-icon-item mi-chevron"></div>
            </div>
        </div>
    </div>
</template>
<style scoped>
.navbar-custom{
    height: 48px;
    display: flex;
    justify-content: space-between;
    align-items: center;
}
.left,.right{
    display: flex;
}
.right{
    padding: 0 20px 0 0;
}
.mi-stripes{
    background-position: -843px -30px;
    margin: 0 10px;
}

.mi-chevron{
    background-position: -80px -360px;
    transform: rotate(90deg);
    padding-top: 10px;
    width: 14px;
    height: 14px;
    min-width: 14px;
    max-width: 14px;
    margin-left: 10px;
}
.header-branch{
    display: flex;
}
.header-branch-name{
    padding-left: 10px;
    font-weight: 600;
    font-size: 14px;
    color: #111;
    line-height: 24px;
}

.mi-bell{
    background-position: -788px -30px;
    margin: 10px;
}

.user-dropdown{
    display: flex;
    align-items: center;
    font-weight: 600;
    justify-content: center;
    align-items: center;
    padding: 0 16px
}
.user-avatar{
    background: #e2e2e2;
    margin-right: 6px;
    border-radius: 15px;
}
.mi-avatar{
    background-position: -352px -894px;
    width: 32px;
    height: 32px;
    min-width: 32px;
    min-height: 32px;
    border-radius: 50%;
}
</style>