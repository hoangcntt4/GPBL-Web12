<template>
    <div>
        <base-input autoFocus />
        
    <toast-message/>
    </div>
</template>
<script>
import BaseInput from '../base/BaseInput.vue'
import ToastMessage from '../base/ToastMessage.vue';
export default {
    components:{BaseInput,ToastMessage  },
    methods:{

         focusInput(){
            this.$refs.input.focus();
        }
    }
}
</script>