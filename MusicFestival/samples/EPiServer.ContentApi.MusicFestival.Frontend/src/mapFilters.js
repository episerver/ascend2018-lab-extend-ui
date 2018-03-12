// copy global filter into local scope, getting around Vue limitation of programmatically using global filter from javascript
// https://github.com/vuejs/Discussion/issues/405#issuecomment-287371191
export default function mapFilters (filters) {
  return filters.reduce((result, filter) => {
    result[filter] = function (...args) {
      return this.$options.filters[filter](...args)
    }
    return result
  }, {})
}
