<script>
import { defineComponent } from "vue"

export default defineComponent({
	props: {
		pageSize: {
			type: Number,
			required: true,
		},
	},
	data(props) {
		return {
			pageSizeValue: props.pageSize,
		}
	},
	methods: {
		handleChange() {
			this.$emit("sizeChange", this.pageSizeValue)
		},
	},
	emits: ["sizeChange"],
})
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
