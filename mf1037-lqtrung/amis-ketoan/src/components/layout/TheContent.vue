<template>
    <div>
        <div class="container">
            <form @submit.prevent="save()">
                <div class="form-group row">
                    <label for="" class="col-sm-3 col-form-label">Product Name</label>
                    <div class="col-sm-9">
                        <input class="form-control m-input" type="text" v-model="product.Name" @blur="validate()" :class="{'is-invalid': errors.name}">
                        <div class="invalid-feedback" v-if="errors.name">{{errors.name}}</div>
                    </div>
                </div>

                <div class="form-group row">
                    <label for="" class="col-sm-3 col-form-label">Product Price</label>
                    <div class="col-sm-9">
                        <input class="form-control m-input" type="text" v-model="product.Price" @blur="validate()" :class="{'is-invalid': errors.price}"> 
                        <div class="invalid-feedback" v-if="errors.price">{{errors.price}}</div>
                    </div>
                </div>
                <div class="col-sm-9">
                    <button type="submit" class="btn btn-primary">Save</button>
                </div>
            </form>
        </div>
    </div>
</template>
<script>
export default {
    data: ()=>({ 
        product: {
            Name: "",
            Price: "",
        },
        errors: {
                name: "",
                price: "",
            }
    }),
    methods:{
        validate(){
            let isValid = true
            this.errors=  {
                name: "",
                price: "",
            }

            if(!this.product.Name){
                this.errors.name = "Product Name is required"
                isValid = false
            }

            if(!this.product.Price){
                this.errors.price = "Product Price is required"
                isValid = false
            }
            else if(!this.isNumber(this.product.Price)){
                this.errors.price = "Product Price là số"
                isValid = false
            }
            return isValid
        },
        isNumber(value){    
            return /^\d*$/.test(value)
        },
        save(){
            if(this.validate()){
                alert("OK")
            }
        }
    }
}
</script>
<style scoped>
.form-control:focus{
    box-shadow: none;
}
</style>