<template>
  <div class="calculate-price-container">
    <!-- Form for price input -->
    <div class="form-container">
      <form @submit.prevent="submitForm">
        <!-- Vehicle Type -->
        <label for="vehicleType">Vehicle Type:</label>
        <select v-model="vehicleType" id="vehicleType" required>
          <option value="Common">Common</option>
          <option value="Luxury">Luxury</option>
        </select>

        <!-- Vehicle Price -->
        <label for="vehiclePrice">Vehicle Price:</label>
        <input type="number" v-model="vehiclePrice" id="vehiclePrice" placeholder="Enter the price" required />

        <!-- Submit Button -->
        <button type="submit">Calculate Price</button>
      </form>
    </div>

    <!-- Price Table -->
    <div class="table-container">
      <table v-if="calculatedPrice != null">
        <thead>
          <tr>
            <th>Vehicle Price ($)</th>
            <th>Vehicle Type</th>
            <th v-for="fee in calculatedPrice.fees">
              {{fee.name}}
            </th>
            <th>Total</th>
          </tr>
        </thead>
        <tbody>
          <tr>
            <td>{{ calculatedPrice.price }}</td>
            <td>{{ calculatedPrice.type }}</td>
            <td v-for="fee in calculatedPrice.fees">
              {{fee.value}}
            </td>
            <td>{{ calculatedPrice.totalPrice }}</td>
          </tr>
        </tbody>
      </table>
    </div>
  </div>
</template>

<script>
  import axios from 'axios';

  export default {
    data() {
      return {
        vehicleType: 'Common',
        vehiclePrice: null,
        calculatedPrice: null
      };
    },
    methods: {

      async submitForm() {

        if (this.vehiclePrice <= 0) {
          alert("Please enter a valid vehicle price!");
          return;
        }

        try {
          //The URL must be by environment DEV, STAGING AND PROD.
          //This request could have a typescript file for it.
          const response = await axios.post('http://localhost:5177/api/1.0/Vehicle/CalculatePrice', {
            vehicleType: this.vehicleType,
            vehiclePrice: this.vehiclePrice
          });

          this.calculatedPrice = response.data;

        } catch (error) {
          console.error("There was an error with the API request:", error);
          alert("Failed to fetch data. Please try again later.");
        }
      }
    }
  };
</script>

<style scoped>
  .calculate-price-container {
    display: flex;
    flex-direction: column;
    align-items: center;
    margin-top: 20px;
    padding: 20px;
  }

  .form-container {
    margin-bottom: 30px;
    padding: 20px;
    border: 1px solid #ddd;
    border-radius: 5px;
    width: 100%;
    max-width: 500px;
  }

  .table-container {
    padding: 20px;
    width: 100%;
    max-width: 900px;
    overflow-x: auto;
  }

  table {
    width: 100%;
    border-collapse: collapse;
    text-align: left;
  }

    table th, table td {
      padding: 10px;
      border: 1px solid #ddd;
    }

  form {
    display: flex;
    flex-direction: column;
  }

    form label {
      margin-bottom: 5px;
    }

    form select,
    form input {
      margin-bottom: 15px;
      padding: 8px;
    }

  button {
    padding: 10px;
    background-color: #337ab7;
    color: white;
    border: none;
    cursor: pointer;
  }

    button:hover {
      background-color: #0056b3;
    }
</style>
