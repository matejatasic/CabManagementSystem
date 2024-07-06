const path = require("path");
const HtmlWebpackPlugin = require("html-webpack-plugin");
const { DefinePlugin, ProvidePlugin } = require("webpack");

const envPath = path.resolve(__dirname, ".env");
const envVariables = require("dotenv").config({ path: envPath }).parsed ?? {};
const NodePolyfillPlugin = require("node-polyfill-webpack-plugin")

module.exports = {
  entry: "./src/index.tsx",
  output: {
    filename: "main.js",
    path: path.resolve(__dirname, "build"),
    publicPath: '/'
  },
  devServer: {
    historyApiFallback: true,
  },
  module: {
    rules: [
      {
        test: /\.(ts|tsx)$/,
        loader: "ts-loader",
        exclude: /node-modules/
      },
      {
        test: /\.css$/,
        use: ["style-loader", "css-loader"]
      },
      {
        test: /\.(sa|sc)ss$/,
        use: ["style-loader", "css-loader", "sass-loader"]
      },
      {
        test : /\.(jpg|png)$/,
        type: 'asset/resource',
      },
    ],
  },
  plugins: [
    new HtmlWebpackPlugin({
      template: './public/index.html',
      filename: 'index.html',
    }),
    new DefinePlugin({
      "process.env": JSON.stringify(envVariables)
    }),
    new NodePolyfillPlugin(),
    new ProvidePlugin({
      process: 'process/browser',
      Buffer: ['buffer', 'Buffer'],
    }),
  ],
  resolve: {
    extensions: [".js", ".jsx", ".ts", ".tsx", ".scss", ".mjs"],
    fallback: {
      "child_process": false,
      "worker_threads": false,
      "uglify-js": false,
      "@swc/core": false,
      "esbuild": false,
      "module": require.resolve("module"),
      "inspector": false,
      "process/browser": require.resolve('process/browser')
    }
  },
  stats: {
    warningsFilter: (warning) => !/Critical dependency/.test(warning),
  },
};
