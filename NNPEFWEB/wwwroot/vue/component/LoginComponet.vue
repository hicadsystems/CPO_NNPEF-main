<template>
  <!-- WRAPPER -->
<div>
                           <div class="row px-3">
                                <label for="" style="margin-top:-18px">User Name</label>
                              <input v-model="postBody.username" name="username" class="mb-4 form-styling" placeholder="" required />
                            </div>
                            <div class="row px-3">
                                 <label for="" style="margin-top:-18px">Password</label>
                                <input v-model="postBody.password" type="password" name="password" class="mb-4 form-styling" placeholder="" required />
                            </div>
                            
                                                
                            <div class="row px-3 mb-4">
                                <div class="custom-control custom-checkbox custom-control-inline">
                                    <input id="chk1" type="checkbox" name="chk" class="custom-control-input"> <label for="chk1" class="custom-control-label text-sm">Remember me</label>
                                </div>

                                <!-- <a @click="forgotPassword" class="ml-auto mb-0 text-sm">Forgot Password?</a> -->
                            </div>
                            <div class="row mb-3 px-3">
                                <button type="submit" class="btn-signin text-center">Login</button>
                            </div>
    
</div>
  </template>
  <script>
    export default {
    props: ["privacyLink", "cookieString"],
    data() {
        return {
            hidden: false,
            commandList:null,
            shipList:null,
             postBody: {
                commandid:'',
                ship:'',
                username:'',
                password:'',
                appointment:'',

            }
        };
    },
    
     mounted () {
        axios
            .get('/api/command/getAllcommand')
            .then(response => {
                this.commandList = response.data
                })
        },
         methods:{
        getship(){
           axios
        .get(`/api/command/getship/${this.postBody.commandid}`)
        .then(response=>(this.shipList=response.data
        ))},
       
       forgotPassword(){

           window.open(
                `/Authentication/ForgotPassword/`
            );
       }
        },
    };
     
</script>