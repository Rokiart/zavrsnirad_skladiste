import axios from "axios";

export const httpService = axios.create({
    baseURL: 'https://romanzaric-001-site1.itempurl.com/',
    headers:{
        'Content-Type' : 'application/json'
    }
});