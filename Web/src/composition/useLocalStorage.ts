import { useEventListener } from "@vueuse/core"
import { shallowRef, watch } from "vue"

export function useLocalStorage<T>(key: string, defaultValue: T, watchOtherTabs = true) {
	const item = shallowRef<T>(defaultValue)

	function setLocalStorage() {
		localStorage.setItem(key, JSON.stringify(item.value))
	}

	const existingValue = localStorage.getItem(key)
	if (existingValue !== null) {
		item.value = JSON.parse(existingValue)
	} else {
		setLocalStorage()
	}

	watch(item, setLocalStorage)

	if (watchOtherTabs) {
		useEventListener("storage", (e) => {
			if (e.storageArea === localStorage && e.key === key) {
				if (e.newValue === null) {
					item.value = defaultValue
					setLocalStorage()
				} else {
					item.value = JSON.parse(e.newValue)
				}
			}
		})
	}

	return item
}
