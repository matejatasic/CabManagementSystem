const path = require("path");
const HtmlWebpackPlugin = require("html-webpack-plugin");
const { DefinePlugin } = require("webpack");

const envPath = path.resolve(__dirname, ".env");
console.log(require("dotenv"))
const envVariables = require("dotenv").config({ path: envPath }).parsed ?? {};

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
      }
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
  ],
  resolve: {
    extensions: [".js", ".jsx", ".ts", ".tsx", ".scss"],
  },
};
