import axios from "axios";

export const httpService =axios.create({
    baseURL: 'http://romanzaric-001-site2.itempurl.com/api/v1' ,
    headers:{
        'Content-Type': 'application/json'
    }
});