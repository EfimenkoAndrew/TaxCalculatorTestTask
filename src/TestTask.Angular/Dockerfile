FROM node:alpine as build
WORKDIR /app
COPY /src/TestTask.Angular/package.json ./
RUN npm install -g @angular/cli
RUN yarn install
COPY /src/TestTask.Angular .
RUN yarn build
EXPOSE 4200
CMD ["ng", "serve", "--host", "0.0.0.0"]


#FROM nginx:alpine
#COPY --from=build /app/dist/test-task.angular /usr/share/nginx/html