<script>
import styles from "./MoviesTable.module.scss";
export default {
	props: {
		movies: {
			type: Array,
			required: true,
		},
	},
	data() {
		return {
			styles,
			sortValue: 0,
			sortDirectionAsc: true
		}
	},
	emits: ["sortChanged", "editButtonClicked", "detailsButtonClicked", "deleteButtonClicked"],
	methods: {
		setSort(sortType) {
			if(this.sortValue === sortType && this.sortDirectionAsc) {
				this.sortDirectionAsc = false;
			}else {
				this.sortDirectionAsc = true;
			}
			this.sortValue = sortType
			let sortInfo = {
				"sortType" :sortType,
				"sortDirectionAsc" : this.sortDirectionAsc
			}
			this.$emit("sortChanged", sortInfo)
		},
		editButtonEmit(movie) {
			this.$emit("editButtonClicked", movie)
		},
		detailsButtonSend(movie) {
			this.$router.push("/movie?id=" + movie.id)
		},
		deleteButtonEmit(movie) {
			this.$emit("deleteButtonClicked", movie)
		},
		genreDisplay(movie) {
			if(movie.genres.length > 2) {
				return `${movie.genres[0].name}, ${movie.genres[1].name} and ${movie.genres.length - 2} more`
			}
			if(movie.genres.length === 2) {
				return `${movie.genres[0].name}, ${movie.genres[1].name}`
			}
			if(movie.genres.length === 1) {
				return `${movie.genres[0].name}`
			}
			return "N/A"
		}
	},
}
</script>

<template>
	<table class="table">
		<thead>
			<tr>
				<th><div :class="styles.tableHeader"><a @click="setSort(1)">Title</a><p v-if="sortValue===1&&sortDirectionAsc">&#9660</p><p v-if="sortValue===1 && !sortDirectionAsc">&#9650</p></div></th>
				<th><div :class="styles.tableHeader"><a @click="setSort(2)">ReleaseDate</a><p v-if="sortValue===2&&sortDirectionAsc">&#9660</p><p v-if="sortValue===2 && !sortDirectionAsc">&#9650</p></div></th>
				<th><div :class="styles.tableHeader"><a @click="setSort(3)">Genre</a><p v-if="sortValue===3&&sortDirectionAsc">&#9660</p><p v-if="sortValue===3 && !sortDirectionAsc">&#9650</p></div></th>
				<th><div :class="styles.tableHeader"><a @click="setSort(4)">Rating</a><p v-if="sortValue===4&&sortDirectionAsc">&#9660</p><p v-if="sortValue===4 && !sortDirectionAsc">&#9650</p></div></th>
				<th><div :class="styles.tableHeader"><a @click="setSort(5)">Price</a><p v-if="sortValue===5&&sortDirectionAsc">&#9660</p><p v-if="sortValue===5 && !sortDirectionAsc">&#9650</p></div></th>
				<th></th>
			</tr>
		</thead>
		<tbody>
			<tr v-for="movie in movies" :key="movie.id">
				<td>{{ movie.title }}</td>
				<td>{{ movie.releaseDate }}</td>
				<td>{{ genreDisplay(movie) }}</td>
				<td>{{ movie.rating }}</td>
				<td>{{ movie.price }}</td>
				<td>
					<a @click="editButtonEmit(movie)">Edit</a> | <a @click="detailsButtonSend(movie)">Details</a> |
					<a @click="deleteButtonEmit(movie)">Delete</a>
				</td>
			</tr>
		</tbody>
	</table>
</template>
