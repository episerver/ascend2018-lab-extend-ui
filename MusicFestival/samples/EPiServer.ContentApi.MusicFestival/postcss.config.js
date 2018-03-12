module.exports = {
    plugins: {
        autoprefixer: { browsers: ['last 3 versions', 'iOS >= 8'] },
        'postcss-pxtorem': {},
        'postcss-color-rgba-fallback': {},
        'css-mqpacker': { sort: true },
        cssnano: {
            //reduceIdents: {
            //    keyframes: false // do not rename keyframes
            //},
            //discardUnused: {
            //    keyframes: false // do not remove unused keyframes
            //},
            zindex: false // Rebases z-index values http://cssnano.co/optimisations/zindex/ which breaks the Modals
        }
    }
};
