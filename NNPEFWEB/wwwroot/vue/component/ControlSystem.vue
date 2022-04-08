<template>
    <!-- WRAPPER -->
    <div>
        <div class="card-body">
            <table id="datatables-buttons" class="table table-striped" style="width:100%">
                <thead>
                    <tr>
                        <th>Year</th>
                        <th>Ship</th>
                        <th>Status</th>
                        <!-- <th>Action</th> -->
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="con in controlList">
                        <td>{{ con.processingyear }}</td>
                        <td>{{ con.ship }}</td>
                        <td>{{ con.status }}</td>

                        <!-- <td><button type="button" class="btn btn-submit btn-primary" @click="processRetrieve(con)">Edit</button></td>
                        <td><button type="button" class="btn btn-submit btn-primary" @click=" processDelete(con.id)">Delete</button></td> -->
                    </tr>
                </tbody>

            </table>
          
        </div>

    </div>

    <!-- END WRAPPER -->
</template>

<script>
export default {

            data() {
                return {
                controlList:null
                };
            },
       
    mounted () {
        axios
            .get('/api/ControlAPI/getcontrol')
            .then(response => {this.controlList = response.data;
             alert(response.data.processingyear);
            })
            
     },

};
</script>