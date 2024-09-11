<script>
import styles from "./GenresTable.module.scss";
export default {
	props: {
		genres: {
			type: Array,
			required: true,
		},
	},
	data() {
		return {
			styles,
			sortDirectionAsc: true
		}
	},
	emits: ["sortChanged", "editButtonClicked", "detailsButtonClicked", "deleteButtonClicked"],
	methods: {
		setSort() {
			if(this.sortDirectionAsc) {
				this.sortDirectionAsc = false;
			}else {
				this.sortDirectionAsc = true;
			}
			this.$emit("sortChanged", this.sortDirectionAsc)
		},
		editButtonEmit(genre) {
			this.$emit("editButtonClicked", genre)
		},
		deleteButtonEmit(genre) {
			this.$emit("deleteButtonClicked", genre)
		},
	},
}
</script>

<template>
	<table class="table">
		<thead>
			<tr>
				<th><div :class="styles.tableHeader"><a @click="setSort()">Name</a><p v-if="sortDirectionAsc">&#9660</p><p v-if="!sortDirectionAsc">&#9650</p></div></th>
				<th></th>
			</tr>
		</thead>
		<tbody>
			<tr v-for="genre in genres" :key="genre.id">
				<td>{{ genre.name }}</td>
				<td>
					<a @click="editButtonEmit(genre)">Edit</a> |
					<a @click="deleteButtonEmit(genre)">Delete</a>
				</td>
			</tr>
		</tbody>
	</table>
</template>
