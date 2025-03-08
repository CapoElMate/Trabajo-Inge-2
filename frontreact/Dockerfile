FROM node:18-alpine as build

WORKDIR /app

COPY package.json .

RUN npm install

COPY . .

RUN npm run build

#front, React

EXPOSE 55205 

CMD ["npm", "run", "dev","--host"]