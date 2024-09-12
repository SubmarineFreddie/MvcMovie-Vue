<script setup lang="ts">
import { ref } from "vue"

interface Emits {
	(e: "size-change", newSize: number): void
}

const emit = defineEmits<Emits>()

const pageSizeValue = ref(5)

function handleChange() {
	emit("size-change", pageSizeValue.value)
}
</script>

<template>
	<p>
		Page Size:
		<select v-model="pageSizeValue" @change="handleChange">
			<option :value="5">5</option>
			<option :value="10">10</option>
			<option :value="15">15</option>
			<option :value="20">20</option>
			<option :value="25">25</option>
		</select>
	</p>
</template>
