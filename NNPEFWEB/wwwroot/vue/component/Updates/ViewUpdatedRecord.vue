<template>
  <!-- WRAPPER -->
  <div>
    <table class="table table-striped table-bordered table-hover">
      <thead>
        <tr>
          <th>Service No.</th>
          <th>Surname</th>
          <th>Other Name</th>
          <th>Rank</th>
          <th>Division Officer Name</th>
          <th>HOD Name</th>
          <th>Commander Name</th>
          <th>Command</th>
          <th>Ship</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="status in statusList" :key="status.serviceNumber">
          <td>{{ status.serviceNumber }}</td>
          <td>{{ status.surname }}</td>
          <td>{{ status.otherName }}</td>
          <td>{{ status.rank }}</td>
          <td>{{ status.div_off_name }}</td>
          <td>{{ status.hod_name }}</td>
          <td>{{ status.cdr_name }}</td>
          <td>{{ status.command }}</td>
          <td>{{ status.ship }}</td>
        </tr>
      </tbody>
    </table>
  </div>

  <!-- END WRAPPER -->
</template>
<script>
export default {
  data() {
    return {
      statusList: null,
      responseMessage: "",
     
    };
  },
  mounted() {
    this.getEFPersonalInfo();
  },
 

  methods: {
    getEFPersonalInfo: function() {
      axios
        .get("/api/Reports/getListPerson")
        .then((response) => (this.statusList = response.data));
    },
  },
};
</script>
