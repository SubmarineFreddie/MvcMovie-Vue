<script setup>
import { RouterView } from "vue-router"
</script>

<template>
	<header>
		<nav class="navbar navbar-expand-sm navbar-toggleable-sm navbar-light bg-white border-bottom box-shadow mb-3">
			<div class="container-fluid">
				<a class="navbar-brand" asp-area="" asp-controller="Movies" asp-action="Index" href="/movielist">Movie List</a>
				<a class="navbar-brand" asp-area="" asp-controller="Genres" asp-action="Index" href="/genrelist">Genre List</a>
				<button
					class="navbar-toggler"
					type="button"
					data-bs-toggle="collapse"
					data-bs-target=".navbar-collapse"
					aria-controls="navbarSupportedContent"
					aria-expanded="false"
					aria-label="Toggle navigation"
				>
					<span class="navbar-toggler-icon"></span>
				</button>
				<div class="navbar-collapse collapse d-sm-inline-flex justify-content-between">
					<ul class="navbar-nav flex-grow-1">
						<li class="nav-item">
							<a class="nav-link text-dark" href="/">Home</a>
						</li>
						<li class="nav-item">
							<a class="nav-link text-dark" href="/privacy">Privacy</a>
						</li>
					</ul>
				</div>
			</div>
		</nav>
	</header>
	<div class="container">
		<main role="main" class="pb-3">
			<RouterView />
		</main>
	</div>

	<footer class="border-top footer text-muted">
		<div class="container">&copy; 2023 - Movie App - <a asp-area="" asp-controller="Home" asp-action="Privacy">Privacy</a></div>
	</footer>
</template>
