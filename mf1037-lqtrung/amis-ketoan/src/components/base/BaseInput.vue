<template>
    <div class="ms-input">
        <input class="m-input m-input-icon" type="text" placeholder="Tìm theo mã, tên nhân viên">
        <div class="mi-icon-item mi-search"></div>
    </div>
</template>

<script>
export default {
    
}
</script>
<style scoped>
.ms-input{
    display: flex;
    align-items: flex-start;
    flex-direction: column;
    position: relative;
    justify-content: center;
}
.m-input{
    font-size: 13px;
    height: 32px;
    color: inherit;
    position: relative;
    border-radius: 2px;
    border: 1px solid #babec5;
    width: 240px;
    padding: 0 44px 0 10px;
}
.mi-search{
    background-position: -992px -360px;
    width: 16px;
    height: 16px;
    min-width: 16px;
    min-height: 16px;
    cursor: pointer;
    position: absolute;
    left: auto;
    right: 12px;
    color: rgba(0,0,0,.4);
}

.m-input:focus{
    outline: none;
    border: 1px solid #2ca01c;
}

::placeholder{
    font-style: italic;
    font-size: 12px;
    color: #BBBBBB;
    
}
</style>