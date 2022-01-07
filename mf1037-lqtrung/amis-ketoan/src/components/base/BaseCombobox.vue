<template>
    <div>
        <select class="m-combobox" name="" id="">
            <option value="">10 bản ghi trên 1 trang</option>
            <option value="">20 bản ghi trên 1 trang</option>
            <option value="">30 bản ghi trên 1 trang</option>
            <option value="">50 bản ghi trên 1 trang</option>
            <option value="">100 bản ghi trên 1 trang</option>
        </select>
    </div>
</template>
<style scoped>
.m-combobox{
    height: 32px;
    display: flex;
    border: 1px solid #babec5;
    border-radius: 2px;
    background-color: #fff;
    cursor: pointer;
    padding: 0 10px;
}
.m-combobox:focus{
    border-color: #2ca01c;
}
</style>