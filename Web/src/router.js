import { createRouter, createWebHistory } from "vue-router"
import GenreListPage from "./pages/Genres/GenreListPage.vue"
import HomePage from "./pages/HomePage.vue"
import MovieDetailsPage from "./pages/Movies/MovieDetailsPage.vue"
import MovieListPage from "./pages/Movies/MovieList/MovieListPage.vue"
import PrivacyPage from "./pages/PrivacyPage.vue"

const router = createRouter({
	history: createWebHistory(import.meta.env.BASE_URL),
	routes: [
		{
			path: "/",
			name: "home",
			component: HomePage,
		},
		{
			path: "/privacy",
			name: "privacy",
			component: PrivacyPage,
		},
		{
			path: "/movielist",
			name: "movieList",
			component: MovieListPage,
		},
		{
			path: "/movie",
			name: "movie",
			component: MovieDetailsPage,
		},
		{
			path: "/genrelist",
			name: "genreList",
			component: GenreListPage,
		},
	],
})

export default router
