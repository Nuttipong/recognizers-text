const path = require('path');
const HtmlWebPackPlugin = require('html-webpack-plugin');
const MiniCssExtractPlugin = require('mini-css-extract-plugin');
const CleanWebpackPlugin = require('clean-webpack-plugin');
const WebpackMd5Hash = require('webpack-md5-hash');

module.exports = {
    resolve: {
        extensions: ['.js', '.map', '.jpg', '.png', '.css', '.scss']
    },
    module: {
        rules: [
            {test: /\.(js|jsx)$/, exclude: /node_modules/, use: {loader: 'babel-loader'}},
            {test: /\.html$/, use: [{loader: 'html-loader', options: {minimize: true}}]},
            {test: /\.(css|scss)$/, use: ['style-loader', MiniCssExtractPlugin.loader, 'css-loader', 'sass-loader']},
            {
                test: /\.(jpe?g|png|gif)$/i,
                use: [
                    "file-loader?name=images/[name]-[hash:base64:5].[ext]",
                    {
                        loader: 'image-webpack-loader',
                        options: {
                            query: {
                                mozjpeg: {
                                    progressive: true,
                                },
                                gifsicle: {
                                    interlaced: true,
                                },
                                optipng: {
                                    optimizationLevel: 7,
                                }
                            },
                        }
                    }
                ]
            },
            {
                test: /\.(woff(2)?|ttf|eot|svg)(\?v=\d+\.\d+\.\d+)?$/,
                use: [{
                    loader: 'file-loader',
                    options: {
                        name: '[name].[ext]',
                        outputPath: 'fonts/',
                        context: path.resolve(__dirname, 'dist')
                    }
                }]
            }
        ]
    },
    output: {
        path: path.resolve(__dirname, 'dist'),
        publicPath: '/',
        filename: '[name].[hash].js'
    },
    plugins: [
        new CleanWebpackPlugin(['dist']),
        new HtmlWebPackPlugin({
          template: 'src/index.html',
          filename: './index.html'
        }),
        new MiniCssExtractPlugin({
            filename: 'style.[hash].css'
        }),
        new WebpackMd5Hash()
    ]
};