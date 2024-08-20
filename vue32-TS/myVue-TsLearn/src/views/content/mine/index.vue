<template>
    <div>{{str}}--{{num}}</div>
</template>

<script setup lang="ts">
import {ref} from 'vue'
const str = ref<string>('mine');
const num = ref<number>(0);
</script>