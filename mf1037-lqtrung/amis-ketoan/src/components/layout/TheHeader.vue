<template>
    <div class="navbar-custom">
        <div class="left">
            <base-icon icon="stripes"></base-icon>
            <div class="header-branch">
                <div class="header-branch-name">CÔNG TY TNHH SẢN XUẤT - THƯƠNG MẠI - DỊCH VỤ QUI PHÚC</div>
                <base-icon :size="14" icon="chevron"></base-icon>
            </div>
        </div>
        <div class="right">
            <base-icon icon="bell"></base-icon>
            <div class="user-dropdown">
                <div class="user-avatar">
                    <base-icon :size="32" icon="avatar"></base-icon>
                </div>
                <div>
                    <div class="user-name">Lê Quang Trung</div>
                </div>
                <base-icon :size="14" icon="chevron2"></base-icon>
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
<style scoped src="../../assets/css/components/theHeader.css">
</style>