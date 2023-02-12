import { Pipe, PipeTransform } from '@angular/core';

export class Constants{
    public static readonly USER_KEY:string="userInfo";

}

let baseUrl = 'https://localhost:7046/api';
export default baseUrl;
