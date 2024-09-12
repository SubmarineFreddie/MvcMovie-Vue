import type { SortDirection } from "./SortDirection"

export interface Genre {
	id: number
	name: string
}

export interface GenreSearchOptions {
	page?: number
	pageSize?: number
	name?: string | null
	sortDirection?: SortDirection
}

export interface GenrePageInfo {
	pageNumber: number
	genres: Genre[]
	pageCount: number
	genreCount: number
}
