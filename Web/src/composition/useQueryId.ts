import { computed } from "vue"
import { useRoute } from "vue-router"

export function useQueryId(name: string) {
	const route = useRoute()
	return computed(() => {
		const id = route.query[name]
		if (id != null && !Array.isArray(id)) {
			return Number.parseInt(id)
		}
		throw new Error("")
	})
}
