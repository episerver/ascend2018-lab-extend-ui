﻿var path = require("path"),
    glob = require("glob"),
    notifier = require("webpack-build-notifier"),
    extractTextPlugin = require("extract-text-webpack-plugin"),
    webpack = require('webpack');

function resolve(dir) {
    return path.join(__dirname, '..', dir)
}
// TODO: Next step, look at path for async modules, right now it tries to load from root.
var config = {
    entry: {
        main: ["./Assets/Scripts/main.js", "./Assets/Styles/Main.less"]
    },
    output: {
        path: path.resolve(__dirname, "./Assets/bundled/"),
        publicPath: "/Assets/bundled/",
        filename: "[name].bundle.js"
    },
    plugins: [
        new extractTextPlugin('[name].bundle.css'), // save the css as external files instead of bundling them into the javascripts
        new webpack.WatchIgnorePlugin([/node_modules/]), // turn off watcher for node_modules and our vueImports.js file created above
    ],
    resolve: {
        alias: {
            vue: 'vue/dist/vue.js',
            '@': path.join(__dirname, 'Assets')
        }
    },
    module: {
        loaders: [{
            // the loaders will be applied from right to left
            test: /\.less$/,
            loader: extractTextPlugin.extract('css-loader!postcss-loader!less-loader!import-glob-loader')
        },
        {
            test: /\.js$/,
            exclude: [/node_modules/],
            use: [{
                loader: 'babel-loader'
            }]
        },
        {
            test: /\.vue$/,
            exclude: [/node_modules/],
            loader: 'vue-loader'
        },
        {
            test: /\.svg$/,
            exclude: /node_modules/,
            use: [
                { loader: 'svg-sprite-loader' }
            ]
        }
        ]
    },
    stats: {
        children: false,
        colors: true
    },
    devtool: 'source-map',
    watchOptions: {

    }
};

if (process.env.NODE_ENV === 'production') {
    // stuff that should only happen when doing a production build
    config.plugins.push(new webpack.optimize.UglifyJsPlugin({
        parallel: {
            cache: true,
            workers: 2
        },
        sourceMap: true,
        mangle: true,
        compress: {
            warnings: false,
            pure_getters: true,
            unsafe: true,
            unsafe_comps: false, // breaks some minification, so turned off!
            screw_ie8: true,
            sequences: true,
            dead_code: true,
            conditionals: true,
            booleans: true,
            unused: true,
            if_return: true,
            join_vars: true,
            drop_console: true
        },
        output: {
            comments: false
        }
    }));
} else {
    // and stuff that happens during development, like the toaster notification.
    config.plugins.push(new notifier({
        title: "EpiServer.ContentApi Webpack Build"
    }));
}

module.exports = config;