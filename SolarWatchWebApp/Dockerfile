FROM node:18-alpine AS build
WORKDIR /app

# Copy package.json and package-lock.json first for better caching
COPY package.json package-lock.json ./

# Install dependencies
RUN npm install

# Copy the rest of the application code
COPY . .

CMD [ "npm", "run", "dev", "--", "--host", "0.0.0.0" ]

EXPOSE 5173