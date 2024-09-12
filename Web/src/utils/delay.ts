/**
 * @param time Time in ms to delay for
 */
export function delay(time: number): Promise<void> {
	return new Promise((resolve) => setTimeout(resolve, time))
}
