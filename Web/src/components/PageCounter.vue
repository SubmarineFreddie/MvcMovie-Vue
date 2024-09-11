<script>
import { defineComponent } from "vue"
import styles from "./PageCounter.module.scss"

export default defineComponent({
	data() {
		return { styles }
	},
	props: {
		pageNumber: {
			type: Number,
			required: true,
		},
		totalPages: {
			type: Number,
			required: true,
		},
	},
	methods: {
		firstPage() {
			this.$emit("pageChange", 1)
		},
		previousPage() {
			this.$emit("pageChange", this.pageNumber - 1)
		},
		nextPage() {
			this.$emit("pageChange", this.pageNumber + 1)
		},
		lastPage() {
			this.$emit("pageChange", this.totalPages)
		},
	},
	emits: ["pageChange"],
	computed: {
		finalPage() {
			return this.pageNumber >= this.totalPages
		},
	},
})
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
