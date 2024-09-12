import type { Genre } from "./Genre"
import type { SortDirection } from "./SortDirection"
import type { DateOnly, Id } from "./UtilTypes"

export enum MovieSortType {
	None = 0,
	Title = 1,
	ReleaseDate = 2,
	Genre = 3,
	Rating = 4,
	Price = 5,
}

export interface Movie {
	id: Id
	title: string
	releaseDate: DateOnly
	price: number
	genres: Genre[]
	rating?: string
	summary?: string
	imageUrl?: string
}

export interface MovieSearchOptions {
	page?: number
	pageSize?: number
	title?: string | null
	genreIds?: number[]
	releasedAfter?: DateOnly
	sortType?: MovieSortType
	sortDirection?: SortDirection
}

export interface MoviePageInfo {
	pageNumber: number
	movies: Movie[]
	pageCount: number
	movieCount: number
}
