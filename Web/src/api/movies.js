import { fetchAndValidate } from "./utils"

export async function getMovies() {
	const response = await fetchAndValidate("/api/movies")
	return await response.json()
}

export async function getMovie(id) {
	const response = await fetchAndValidate(`/api/movies/${id}`)
	return await response.json()
}

export async function createMovie(body) {
	const response = await fetchAndValidate("/api/movies", {
		method: "POST",
		body: JSON.stringify(body),
		headers: {
			"Content-Type": "application/json",
		},
	})
	return await response.json()
}

export async function updateMovie(body) {
	const response = await fetchAndValidate("/api/movies", {
		method: "PUT",
		body: JSON.stringify(body),
		headers: {
			"Content-Type": "application/json",
		},
	})
	return await response.json()
}

export async function deleteMovie(body) {
	const response = await fetchAndValidate("/api/movies", {
		method: "DELETE",
		body: JSON.stringify(body),
		headers: {
			"Content-Type": "application/json",
		},
	})
	return await response.json()
}

export async function searchMovies(body) {
	const response = await fetchAndValidate("/api/movies/search", {
		method: "POST",
		body: JSON.stringify(body),
		headers: {
			"Content-Type": "application/json",
		},
	})
	return await response.json()
}
