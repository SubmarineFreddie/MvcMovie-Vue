<script>
import { defineComponent } from "vue"
import styles from "./ExtraListContent.module.scss"

export default defineComponent({
	data() {
		return { styles }
	},
	props: {
		totalPages: {
			type: Number,
			required: true,
		},
		totalItems: {
			type: Number,
			required: true,
		},
		itemType: {
			type: String,
			required: true,
		},
	},
	methods: {
		handleRefresh(event) {
			event.preventDefault()
			this.$emit("refresh")
		},
	},
	emits: ["refresh"],
})
</script>

<template>
	<div :class="styles.extraContent">
		<input @click="handleRefresh" class="btn btn-primary" type="submit" value="Refresh" />
		<div :class="styles.metrics">
			<p>Total Pages: {{ totalPages }}</p>
			<p>Total {{ itemType }}: {{ totalItems }}</p>
		</div>
	</div>
</template>
