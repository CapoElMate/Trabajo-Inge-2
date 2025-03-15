FROM node:18-alpine as build

WORKDIR /app

COPY package*.json .
RUN npm install
RUN npm install dotenv --save-dev


COPY . .
COPY .env ./

# Incrementar el límite de memoria para la compilación
ENV NODE_OPTIONS="--max-old-space-size=4096"

ARG VITE_APP_PUERTO=5173
ENV VITE_APP_PUERTO=${VITE_APP_PUERTO}

EXPOSE ${VITE_APP_PUERTO}

CMD ["npm", "run", "dev","--host"]