<script setup lang="ts">
import type { Movie } from "@/models"
import { ref } from "vue"
import { useRouter } from "vue-router"
import styles from "./MoviesTable.module.scss"

interface Props {
	movies: Movie[]
}

interface Emits {
	(e: "sort-changed", sortType: number, isSortDirectionAsc: boolean): void
	(e: "edit-button-clicked", movie: Movie): void
	(e: "details-button-clicked", movie: Movie): void
	(e: "delete-button-clicked", movie: Movie): void
}

const props = defineProps<Props>()
const emit = defineEmits<Emits>()

const router = useRouter()
const sortValue = ref(0)
const sortDirectionAsc = ref(true)

function setSort(sortType: number) {
	if (sortValue.value === sortType && sortDirectionAsc.value) {
		sortDirectionAsc.value = false
	} else {
		sortDirectionAsc.value = true
	}
	sortValue.value = sortType
	emit("sort-changed", sortType, sortDirectionAsc.value)
}

function editButtonEmit(movie: Movie) {
	emit("edit-button-clicked", movie)
}

function detailsButtonSend(movie: Movie) {
	router.push("/movie?id=" + movie.id)
}

function deleteButtonEmit(movie: Movie) {
	emit("delete-button-clicked", movie)
}

function genreDisplay(movie: Movie) {
	if (movie.genres.length > 2) {
		return `${movie.genres[0].name}, ${movie.genres[1].name} and ${movie.genres.length - 2} more`
	}
	if (movie.genres.length === 2) {
		return `${movie.genres[0].name}, ${movie.genres[1].name}`
	}
	if (movie.genres.length === 1) {
		return `${movie.genres[0].name}`
	}
	return "N/A"
}
</script>

<template>
	<table class="table">
		<thead>
			<tr>
				<th>
					<div :class="styles.tableHeader">
						<a @click="setSort(1)">Title</a>
						<p v-if="sortValue === 1 && sortDirectionAsc">&#9660;</p>
						<p v-if="sortValue === 1 && !sortDirectionAsc">&#9650;</p>
					</div>
				</th>
				<th>
					<div :class="styles.tableHeader">
						<a @click="setSort(2)">ReleaseDate</a>
						<p v-if="sortValue === 2 && sortDirectionAsc">&#9660;</p>
						<p v-if="sortValue === 2 && !sortDirectionAsc">&#9650;</p>
					</div>
				</th>
				<th>
					<div :class="styles.tableHeader">
						<a @click="setSort(3)">Genre</a>
						<p v-if="sortValue === 3 && sortDirectionAsc">&#9660;</p>
						<p v-if="sortValue === 3 && !sortDirectionAsc">&#9650;</p>
					</div>
				</th>
				<th>
					<div :class="styles.tableHeader">
						<a @click="setSort(4)">Rating</a>
						<p v-if="sortValue === 4 && sortDirectionAsc">&#9660;</p>
						<p v-if="sortValue === 4 && !sortDirectionAsc">&#9650;</p>
					</div>
				</th>
				<th>
					<div :class="styles.tableHeader">
						<a @click="setSort(5)">Price</a>
						<p v-if="sortValue === 5 && sortDirectionAsc">&#9660;</p>
						<p v-if="sortValue === 5 && !sortDirectionAsc">&#9650;</p>
					</div>
				</th>
				<th></th>
			</tr>
		</thead>
		<tbody>
			<tr v-for="movie in props.movies" :key="movie.id">
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
