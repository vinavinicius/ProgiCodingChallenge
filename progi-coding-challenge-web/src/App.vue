<template>
  <div class="container">
    <Header />
    <div class="body-container">
      <CalculatePrice />
    </div>
  </div>
</template>

<script>
  import Header from './components/Header.vue'
  import CalculatePrice from './components/vehicle/CalculatePrice.vue'

  export default {
    components: {
      Header,
      CalculatePrice
    }
  }
</script>
