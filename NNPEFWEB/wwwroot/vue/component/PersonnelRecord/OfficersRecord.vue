<template>
  <!-- WRAPPER -->
<div>
                      <input v-model="id" class="form-control" hidden />
                    <div class="row">
                        <div class="col-md-4">
                            <label class="control-label">Service Number</label>
                            <input v-model="serviceNumber" class="form-control" readonly />
                            <span asp-validation-for="serviceNumber" class="text-danger"></span>
                        </div>
                        <div class="col-md-4">
                            <label asp-for="Surname" class="control-label"></label>
                            <input v-model="Surname" class="form-control" />
                            <span asp-validation-for="Surname" class="text-danger"></span>
                        </div>
                        <div class="col-md-4">
                            <label asp-for="OtherName" class="control-label"></label>
                            <input v-model="OtherName" class="form-control" />
                            <span asp-validation-for="OtherName" class="text-danger"></span>
                        </div>
                    </div>
                    <div class="row">

                        <div class="col-md-4">
                            <label  class="control-label">Rank</label>
                            <div class="col-xs-12">
                               <select class="form-control" v-model="postBody.commandid" name="command" @change="getship">
                                  <option v-for="comd in commandList" v-bind:value="comd.id" v-bind:key="comd.id">{{comd.commandName}}</option>
                               </select>
                            </div>
                            </div>
                        

                        <div class="col-md-4">
                            <label class="control-label">BirthDate</label>
                            <input v-model="Birthdate" class="form-control" id="datepicker" type="text" />
                           
                        </div>
                        <div class="col-md-4">
                            <label class="control-label">Date Of Joining</label>
                            <input v-model="DateEmpl" class="form-control" id="datepicker1" type="text" />
                            
                        </div>

                    </div>
                    <div class="row">

                        <div class="col-md-6">
                            <label class="control-label">Residental Address</label>
                            <input v-model="home_address" class="form-control" />
                           
                        </div>
                        <div class="col-md-3">
                            <label class="control-label">Seniority On Current Rank</label>
                            <input v-model="seniorityDate" class="form-control" id="datepicker2" type="text" />
                            <span asp-validation-for="seniorityDate" class="text-danger"></span>
                        </div>
                        <div class="col-md-3">
                            <label class="control-label">Run Out Date</label>
                            <input v-model="runOutDate" class="form-control" id="datepicker3" type="text" />
                            <span asp-validation-for="runOutDate" class="text-danger"></span>
                        </div>
                    </div>
                    <div class="row">


                        <div class="col-md-4">
                            <label class="control-label">Branch</label>
                            <div class="col-xs-12">
                               <select class="form-control" v-model="postBody.commandid" name="command" @change="getship">
                                  <option v-for="comd in commandList" v-bind:value="comd.id" v-bind:key="comd.id">{{comd.commandName}}</option>
                               </select>
                            </div>
                           
                        </div>
                        <div class="col-md-4">
                            <label class="control-label">Command</label>
                            <select class="form-control" v-model="postBody.commandid" name="command" @change="getship">
                                  <option v-for="comd in commandList" v-bind:value="comd.id" v-bind:key="comd.id">{{comd.commandName}}</option>
                            </select>
                        </div>
                       
                        <div class="col-md-4">
                            <label class="control-label">Ship</label>
                           <div class="form-group">
                          <div class="col-xs-12 row px-3">
                           <select class="mb-4 form-styling form-control" v-model="postBody.ship" name="ship">
                             <option v-for="ship in shipList" v-bind:value="ship.id" v-bind:key="ship.id">{{ship.shipName}}</option>
                            </select>
                            </div>
                        </div>
                        </div>
                    </div>



                    <div class="row">
                         <div class="col-md-4">
                            <label class="control-label">Specialisation</label>
                            <div class="col-xs-12">
                               <select class="form-control" v-model="postBody.commandid" name="command" @change="getship">
                                  <option v-for="comd in commandList" v-bind:value="comd.id" v-bind:key="comd.id">{{comd.commandName}}</option>
                               </select>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <label class="control-label">Present Appointment</label>
                            <input v-model="appointment" class="form-control" />
                            <span asp-validation-for="appointment" class="text-danger"></span>
                        </div>

                        <div class="col-md-4">
                            <label class="control-label">State of Origin</label>
                            <select class="form-control" v-model="postBody.commandid" name="command" @change="getship">
                                  <option v-for="comd in commandList" v-bind:value="comd.id" v-bind:key="comd.id">{{comd.commandName}}</option>
                            </select>
                        </div>

                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <label class="control-label">LGA</label>
                                <select class="form-control" v-model="postBody.commandid" name="command" @change="getship">
                                  <option v-for="comd in commandList" v-bind:value="comd.id" v-bind:key="comd.id">{{comd.commandName}}</option>
                               </select>
                        </div>
                        <div class="col-md-4">
                            <label class="control-label">Religion</label>
                            @*<input v-model="religion" class="form-control" />*@
                            <select v-model="religion" class="form-control">
                                <option>Select....</option>
                                <option value="Christian">Christian</option>
                                <option value="Muslim">Muslim</option>

                            </select>
                        </div>
                        <div class="col-md-4">
                            <label class="control-label">GSM Number 1</label>
                            <input v-model="gsm_number" class="form-control" />
                            <span asp-validation-for="gsm_number" class="text-danger"></span>
                        </div>



                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <label class="control-label">GSM Number 2</label>
                            <input v-model="gsm_number2" class="form-control" />
                            <span asp-validation-for="gsm_number2" class="text-danger"></span>
                        </div>
                        <div class="col-md-4">
                            <label class="control-label">Email</label>
                            <input v-model="email" class="form-control" />
                            <span asp-validation-for="email" class="text-danger"></span>
                        </div>
                        <div class="col-md-4">
                            <label asp-for="MaritalStatus" class="control-label"></label>
                            @*<input v-model="MaritalStatus" class="form-control" />*@
                            <select v-model="MaritalStatus" class="form-control">
                                <option>Select....</option>
                                <option value="Single">Single</option>
                                <option value="Married">Married</option>
                                <option value="Divorse">Divorce</option>

                            </select>
                        </div>
                    </div>
                    <hr />

                    <div class="row">
                        <h2 style="">
                            <label class="control-label">Children</label>
                        </h2>
                    </div>
                    <div class="row">
                        <div class="col-md-3">

                            <input v-model="chid_name" class="form-control" placeholder="Child 1" />
                            <span asp-validation-for="chid_name" class="text-danger"></span>
                        </div>
                        <div class="col-md-3">
                            <input v-model="chid_name2" class="form-control" placeholder="Child 2" />
                            <span asp-validation-for="chid_name2" class="text-danger"></span>
                        </div>
                        <div class="col-md-3">
                            <input v-model="chid_name3" class="form-control" placeholder="Child 3" />
                            <span asp-validation-for="chid_name3" class="text-danger"></span>
                        </div>
                        <div class="col-md-3">
                            <input v-model="chid_name4" class="form-control" placeholder="Child 4" />
                            <span asp-validation-for="chid_name4" class="text-danger"></span>
                        </div>
                    </div>
                    <hr />
                    <div class="row">
                        <h2 style="">
                            <label class="control-label">Spouse</label>
                        </h2>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <label class="control-label">Name <i>State Rank/ SVC No. If Personnel</i></label>
                            <input v-model="sp_name" class="form-control" />
                            <span asp-validation-for="sp_name" class="text-danger"></span>
                        </div>
                        <div class="col-md-4">
                            <label class="control-label">Phone</label>
                            <input v-model="sp_phone" class="form-control" />
                            <span asp-validation-for="sp_phone" class="text-danger"></span>
                        </div>
                        <div class="col-md-4">
                            <label class="control-label">Phone 2</label>
                            <input v-model="sp_phone2" class="form-control" />
                            <span asp-validation-for="sp_phone2" class="text-danger"></span>
                        </div>
                        <div class="col-md-4">
                            <label class="control-label">Spouse Email</label>
                            <input v-model="sp_email" class="form-control" />
                            <span asp-validation-for="sp_email" class="text-danger"></span>
                        </div>
                    </div>
                    <hr />
                    <div class="row">
                        <h2 style="">
                            <label class="control-label">Details of Next Of Kin</label>
                        </h2>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <label class="control-label">Name</label>
                            <input v-model="nok_name" class="form-control" />
                            <span asp-validation-for="nok_name" class="text-danger"></span>
                        </div>
                        <div class="col-md-4">
                            <label class="control-label">Relationship</label>
                               <select class="form-control" v-model="postBody.commandid" name="command" @change="getship">
                                  <option v-for="comd in commandList" v-bind:value="comd.id" v-bind:key="comd.id">{{comd.commandName}}</option>
                               </select>
                        </div>
                        <div class="col-md-4">
                            <label class="control-label">Address</label>
                            <input v-model="nok_address" class="form-control" />
                            <span asp-validation-for="nok_address" class="text-danger"></span>
                        </div>

                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <label class="control-label">Phone Number</label>
                            <input v-model="nok_phone" class="form-control" />
                            <span asp-validation-for="nok_phone" class="text-danger"></span>
                        </div>
                        <div class="col-md-4">
                            <label class="control-label">Email</label>
                            <input v-model="nok_email" class="form-control" />
                            <span asp-validation-for="nok_email" class="text-danger"></span>
                        </div>
                        <div class="col-md-4">
                            <label class="control-label">National Identification No</label>
                            <input v-model="nok_nationalId" class="form-control" />
                            <span asp-validation-for="nok_nationalId" class="text-danger"></span>
                        </div>


                    </div>


                    <div class="row">
                        <h2 style="">
                            <label class="control-label">Alternate Next Of Kin</label>
                        </h2>
                    </div>
                    <div class="row">

                        <div class="col-md-4">
                            <label class="control-label">Name</label>
                            <input v-model="nok_name2" class="form-control" />
                            <span asp-validation-for="nok_name2" class="text-danger"></span>
                        </div>
                        <div class="col-md-4">
                            <label class="control-label">Relationship</label>
                               <select class="form-control" v-model="postBody.commandid" name="command" @change="getship">
                                  <option v-for="comd in commandList" v-bind:value="comd.id" v-bind:key="comd.id">{{comd.commandName}}</option>
                               </select>
                        </div>
                        <div class="col-md-4">
                            <label class="control-label">Address</label>
                            <input v-model="nok_address2" class="form-control" />
                            <span asp-validation-for="nok_address2" class="text-danger"></span>
                        </div>


                    </div>
                    <div class="row">

                        <div class="col-md-4">
                            <label class="control-label">Phone Number</label>
                            <input v-model="nok_phone2" class="form-control" />
                            <span asp-validation-for="nok_phone2" class="text-danger"></span>
                        </div>
                        <div class="col-md-4">
                            <label class="control-label">Email</label>
                            <input v-model="nok_email2" class="form-control" />
                            <span asp-validation-for="nok_email2" class="text-danger"></span>
                        </div>
                        <div class="col-md-4">
                            <label class="control-label">National Identification No</label>
                            <input v-model="nok_nationalId2" class="form-control" />
                            <span asp-validation-for="nok_nationalId2" class="text-danger"></span>
                        </div>
                    </div>
                    <hr />

                    <div class="row">
                        <h2 style="">
                            <label class="control-label">Do You Draw</label>
                        </h2>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <label class="control-label"> Rent Subsidy</label>
                        </div>
                        <div class="col-md-2">
                            <input type="radio" id="rentcheck" value="Yes" v-model="rent_subsidy" style="margin-left:20px;margin-right:10px;" />   <label class="control-label">Yes</label>
                        </div>
                        <div class="col-md-2">
                            <input type="radio" id="rentcheck" value="No" v-model="rent_subsidy" style="margin-left:20px;margin-right:10px;" /><label class="control-label">No</label>
                        </div>
                        <span asp-validation-for="rent_subsidy" class="text-danger"></span>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <label class="control-label"> Call Shift duty allow</label>
                        </div>
                        <div class="col-md-2">
                            <input type="radio" id="calldutycheck" value="Yes" v-model="shift_duty_allow" style="margin-left:20px;margin-right:10px;" />   <label class="control-label">Yes</label>
                        </div>
                        <div class="col-md-2">
                            <input type="radio" id="calldutycheck" value="No" v-model="shift_duty_allow" style="margin-left:20px;margin-right:10px;" /><label class="control-label">No</label>
                        </div>
                        <span asp-validation-for="shift_duty_allow" class="text-danger"></span>
                    </div>

                    <div class="row">
                        <div class="col-md-4">
                            <label class="control-label"> Pilot Allow</label>
                        </div>
                        <div class="col-md-2">
                            <input type="radio" id="pilot_allowcheck" value="Yes" v-model="pilot_allow" style="margin-left:20px;margin-right:10px;" />   <label class="control-label">Yes</label>
                        </div>
                        <div class="col-md-2">
                            <input type="radio" id="pilot_allowcheck" value="No" v-model="pilot_allow" style="margin-left:20px;margin-right:10px;" /><label class="control-label">No</label>
                        </div>
                        <span asp-validation-for="pilot_allow" class="text-danger"></span>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <label class="control-label"> Aircrew Allow</label>
                        </div>
                        <div class="col-md-2">
                            <input type="radio" id="aircrew_allowcheck" value="Yes" v-model="aircrew_allow" style="margin-left:20px;margin-right:10px;" />   <label class="control-label">Yes</label>
                        </div>
                        <div class="col-md-2">
                            <input type="radio" id="aircrew_allowcheck" value="No" v-model="aircrew_allow" style="margin-left:20px;margin-right:10px;" /><label class="control-label">No</label>
                        </div>
                        <span asp-validation-for="aircrew_allow" class="text-danger"></span>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <label class="control-label"> SBS Allow</label>
                        </div>
                        <div class="col-md-2">
                            <input type="radio" id="sbscheck" value="Yes" v-model="SBC_allow" style="margin-left:20px;margin-right:10px;" />   <label class="control-label">Yes</label>
                        </div>
                        <div class="col-md-2">
                            <input type="radio" id="sbscheck" value="No" v-model="SBC_allow" style="margin-left:20px;margin-right:10px;" /><label class="control-label">No</label>
                        </div>
                        <span asp-validation-for="SBC_allow" class="text-danger"></span>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <label class="control-label"> Hazard Allow</label>
                        </div>
                        <div class="col-md-2">
                            <input type="radio" id="harcheck" value="Yes" v-model="hazard_allow" style="margin-left:20px;margin-right:10px;" />   <label class="control-label">Yes</label>
                        </div>
                        <div class="col-md-2">
                            <input type="radio" id="harcheck" value="No" v-model="hazard_allow" style="margin-left:20px;margin-right:10px;" /><label class="control-label">No</label>
                        </div>
                        <span asp-validation-for="hazard_allow" class="text-danger"></span>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <label class="control-label"> Special Forces Allow</label>
                        </div>
                        <div class="col-md-2">
                            <input type="radio" id="sfacheck" value="Yes" v-model="special_forces_allow" style="margin-left:20px;margin-right:10px;" />   <label class="control-label">Yes</label>
                        </div>
                        <div class="col-md-2">
                            <input type="radio" id="sfacheck" value="No" v-model="special_forces_allow" style="margin-left:20px;margin-right:10px;" /><label class="control-label">No</label>
                        </div>
                        <span asp-validation-for="special_forces_allow" class="text-danger"></span>
                    </div>

                    <div class="row">
                        <div class="col-md-4">
                            <label class="control-label">Other Allow <i>Please Specify</i></label>
                        </div>
                        <div class="col-md-6">
                            <input v-model="other_allow" class="form-control" />
                            <span asp-validation-for="other_allow" class="text-danger"></span>
                        </div>
                    </div>
                    <hr />
                    <div class="row">
                        <h2 style="">
                            <label class="control-label">Are You Indepted to the following?? If Yes, Please Indicate Your Year Granted in the Text Box</label>
                        </h2>
                    </div>

                    <div class="row">
                        <div class="col-md-4">
                            <label class="control-label">Federal Govt. Staff Housing Loan Scheme</label>
                        </div>
                        <div class="col-md-2">
                            <input type="radio" id="fgscheck" value="Yes" v-model="FGSHLS_loan" style="margin-left:20px;margin-right:10px;" />   <label class="control-label">Yes</label>
                        </div>
                        <div class="col-md-2">
                            <input type="radio" id="fgscheckno" value="No" v-model="FGSHLS_loan" style="margin-left:20px;margin-right:10px;" />   <label class="control-label">No</label>
                        </div>
                        @if (Model.FGSHLS_loan == "Yes")
                        {
                            <div class="col-md-3">
                                <input v-model="FGSHLS_loanYear" class="form-control" />
                            </div>
                        }
                        <div class="col-md-3" id="fedhousing">
                            <input v-model="FGSHLS_loanYear" id="fgshls_loan_text" class="form-control" />
                            <span asp-validation-for="FGSHLS_loan" class="text-danger"></span>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <label class="control-label">CNS Car Refurbishing Loan</label>
                        </div>
                        <div class="col-md-2">
                            <input type="radio" id="cnslcheck" value="Yes" v-model="car_loan" style="margin-left:20px;margin-right:10px;" />   <label class="control-label">Yes</label>
                        </div>
                        <div class="col-md-2">
                            <input type="radio" id="cnslcheckno" value="No" v-model="car_loan" style="margin-left:20px;margin-right:10px;" />   <label class="control-label">No</label>
                        </div>

                            <div class="col-md-3">
                                <input v-model="car_loanYear" class="form-control" />
                            </div>

                        <div class="col-md-4" id="car_loanid">
                            <input v-model="car_loanYear" id="car_loan_text" class="form-control" />
                            <span asp-validation-for="car_loan" class="text-danger"></span>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <label class="control-label">Welfare Loan</label>
                        </div>
                        <div class="col-md-2">
                            <input type="radio" id="wlcheck" value="Yes" v-model="welfare_loan" style="margin-left:20px;margin-right:10px;" />   <label class="control-label">Yes</label>
                        </div>
                        <div class="col-md-2">
                            <input type="radio" id="wlcheckno" value="No" v-model="welfare_loan" style="margin-left:20px;margin-right:10px;" />   <label class="control-label">No</label>
                        </div>

                            <div class="col-md-3">
                                <input v-model="welfare_loanYear" class="form-control" />
                            </div>

                        <div class="col-md-4" id="welfare_loanid">
                            <input v-model="welfare_loanYear" id="welfare_loan_text" class="form-control" />
                            <span asp-validation-for="welfare_loan" class="text-danger"></span>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <label class="control-label">NNMCS loan</label>
                        </div>
                        <div class="col-md-2">
                            <input type="radio" id="nnmcheck" value="Yes" v-model="NNNCS_loan" style="margin-left:20px;margin-right:10px;" />   <label class="control-label">Yes</label>
                        </div>
                        <div class="col-md-2">
                            <input type="radio" id="nnmcheckno" value="No" v-model="NNNCS_loan" style="margin-left:20px;margin-right:10px;" />   <label class="control-label">No</label>
                        </div>

                            <div class="col-md-3">
                                <input v-model="NNNCS_loanYear" class="form-control" />
                            </div>

                        <div class="col-md-3" id="NNNCS_loanid">
                            <input v-model="NNNCS_loanYear" id="NNNCS_loan_text" class="form-control" />
                            <span asp-validation-for="NNNCS_loan" class="text-danger"></span>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <label class="control-label">NNMFBL loan</label>
                        </div>
                        <div class="col-md-2">
                            <input type="radio" id="nnmfcheck" value="Yes" v-model="NNMFBL_loan" style="margin-left:20px;margin-right:10px;" />   <label class="control-label">Yes</label>
                        </div>
                        <div class="col-md-2">
                            <input type="radio" id="nnmfcheckno" value="No" v-model="NNMFBL_loan" style="margin-left:20px;margin-right:10px;" />   <label class="control-label">No</label>
                        </div>

                            <div class="col-md-3">
                                <input v-model="NNMFBL_loanYear" class="form-control" />
                            </div>

                        <div class="col-md-3" id="NNMFBL_loanid">
                            <input v-model="NNMFBL_loanYear" id="NNMFBL_loan_text" class="form-control" />
                            <span asp-validation-for="NNMFBL_loan" class="text-danger"></span>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <label class="control-label">Presidental Pensioner Car Finance Scheme</label>
                        </div>
                        <div class="col-md-2">
                            <input type="radio" id="ppccheck" value="Yes" v-model="PPCFS_loan" style="margin-left:20px;margin-right:10px;" />   <label class="control-label">Yes</label>
                        </div>
                        <div class="col-md-2">
                            <input type="radio" id="ppccheckno" value="No" v-model="PPCFS_loan" style="margin-left:20px;margin-right:10px;" />   <label class="control-label">No</label>
                        </div>

                            <div class="col-md-3">
                                <input v-model="PPCFS_loanYear" class="form-control" />
                            </div>

                        <div class="col-md-3" id="ppc_loanid">
                            <input v-model="PPCFS_loanYear" id="ppc_loan_text" class="form-control" />
                            <span asp-validation-for="PPCFS_loan" class="text-danger"></span>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <label class="control-label">Any Other loan</label>
                        </div>
                        <div class="col-md-2">
                            <input type="radio" id="anyolcheck" value="Yes" v-model="Anyother_Loan" style="margin-left:20px;margin-right:10px;" />   <label class="control-label">Yes</label>
                        </div>
                        <div class="col-md-2">
                            <input type="radio" id="anyolcheckno" value="No" v-model="Anyother_Loan" style="margin-left:20px;margin-right:10px;" />   <label class="control-label">No</label>
                        </div>
                      
                            <div class="col-md-3">
                                <input v-model="Anyother_LoanYear" class="form-control" />
                            </div>

                        <div class="col-md-4" id="AnyOther_loanid">
                            <input v-model="Anyother_LoanYear" id="Anyother_loan_text" class="form-control" />
                            <span asp-validation-for="Anyother_Loan" class="text-danger"></span>
                        </div>
                    </div>
                    <hr />
                    <div class="row">
                        <h2 style="">
                            <label class="control-label">Bank Details</label>
                        </h2>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <label class="control-label">Salary Account Name</label>
                            <input v-model="AccountName" class="form-control" />
                            <span asp-validation-for="AccountName" class="text-danger"></span>
                        </div>
                        <div class="col-md-4">
                            <label class="control-label">Bank Name</label>
                             <select class="form-control" v-model="postBody.commandid" name="command" @change="getship">
                                  <option v-for="comd in commandList" v-bind:value="comd.id" v-bind:key="comd.id">{{comd.commandName}}</option>
                               </select>
                        </div>
                        <div class="col-md-4">
                            <label asp-for="bankbranch" class="control-label"></label>
                            <input v-model="bankbranch" class="form-control" />
                            <span asp-validation-for="bankbranch" class="text-danger"></span>
                        </div>
                        <div class="col-md-4">
                            <label asp-for="BankACNumber" class="control-label"></label>
                            <input v-model="BankACNumber" class="form-control" />
                            <span asp-validation-for="BankACNumber" class="text-danger"></span>
                        </div>

                    </div>
                   
                    <hr />
                    <div class="row">
                        <div class="col-md-4">
                            <input type="submit" value="Update" class="btn btn-primary" />
                        </div>
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
                 
         formNumber:'',
         serviceNumber:'',
         Surname:'',
         OtherName:'',
         Title:'',
         Rank:'',
         Passport:'',
         passports:'',
         NokPassport:'',
         nokpassports:'',
         AltNokPassport:'',
         altnokpassports:'',
         Sex:'',
         MaritalStatus:'',
         Birthdate:'',
         religion:'',
         gsm_number:'',
         gsm_number2:'',
         email:'',
         home_address:'',
         AccountName:'',
         Bankcode:'',
         bankbranch:'',
         BankACNumber:'',
         pfacode:'',
         specialisation:'',
         command:'',
         branch:'',
         appointment:'',
         yearOfAdvancement:'',
         ship:'',
         qualification:'',
         division:'',
         category:'',
         exittype:'',
        DateEmpl:'',
        runOutDate:'',
        DateLeft:'',
        seniorityDate:'',
         yearOfPromotion:'',
         expirationOfEngagementDate:'',
         StateofOrigin:'',
         LocalGovt:'',
         TaxCode:'',
         nok_address:'',
         nok_relation:'',
         nok_phone:'',
         nok_email:'',
         nok_nationalId:'',
         nok_name:'',

         nok_address2:'',
         nok_relation2:'',
         nok_phone2:'',
         nok_email2:'',
         nok_nationalId2:'',
         nok_name2:'',

         sp_name:'',
         sp_phone:'',
         sp_phone2:'',
         sp_email:'',
         chid_name:'',
         chid_name2:'',
         chid_name3:'',
         chid_name4:'',

         entry_mode:'',
         Status:'',
         taxed:'',
         gradelevel:'',
         gradetype:'',
         entitlement:'',
         town:'',
         accomm_type:'',
         GBC:'',
         GBC_Number:'',
         aircrew_allow:'',
         pilot_allow:'',
         shift_duty_allow:'',
         hazard_allow:'',
         rent_subsidy:'',
         SBC_allow:'',
         special_forces_allow:'',
         other_allow:'',

         NSITFcode:'',
         NHFcode:'',
         FGSHLS_loan:'',
         car_loan:'',
         welfare_loan:'',
         NNNCS_loan:'',
         NNMFBL_loan:'',
         PPCFS_loan:'',
         Anyother_Loan:'',

         NSITFcodeYear:'',
         NHFcodeYear:'',
         FGSHLS_loanYear:'',
         car_loanYear:'',
         welfare_loanYear:'',
         NNNCS_loanYear:'',
         NNMFBL_loanYear:'',
         PPCFS_loanYear:'',
         Anyother_LoanYear:'',

         div_off_name:'',
         div_off_rank:'',
         div_off_svcno:'',
  div_off_date:'',
         hod_name:'',
         hod_rank:'',
         hod_svcno:'',
  hod_date:'',
         cdr_name:'',
         cdr_rank:'',
         cdr_svcno:'',
  cdr_date:'',

         payrollclass:'',
         emolumentform:'',
         createdby:'',
  datecreated:'',
  dateModify:'',
  dateVerify:'',
  verifyBy:'',
  dateconfirmed:'',
         confirmedBy:'',
   logo:'',

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
       
        },
    };
     
</script>