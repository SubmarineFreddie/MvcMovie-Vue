<script setup lang="ts">
import { computed } from "vue"
import styles from "./PageCounter.module.scss"

interface Props {
	pageNumber: number
	totalPages: number
}

interface Emits {
	(e: "page-change", pageNumber: number): void
}

const props = defineProps<Props>()
const emit = defineEmits<Emits>()

const finalPage = computed(() => props.pageNumber >= props.totalPages)

function firstPage() {
	emit("page-change", 1)
}
function previousPage() {
	emit("page-change", props.pageNumber - 1)
}
function nextPage() {
	emit("page-change", props.pageNumber + 1)
}
function lastPage() {
	emit("page-change", props.totalPages)
}
</script>

<template>
	<div :class="styles.pageCounter">
		<input :disabled="pageNumber < 2" @click="firstPage" type="button" value="<--" />
		<input :disabled="pageNumber < 2" @click="previousPage" type="button" value="<" />
		<p>{{ pageNumber }}</p>
		<input :disabled="finalPage" @click="nextPage" type="button" value=">" />
		<input :disabled="finalPage" @click="lastPage" type="button" value="-->" />
	</div>
</template>
