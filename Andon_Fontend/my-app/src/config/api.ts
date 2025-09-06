import axios from './axios-customize';
import type { IBackendRes, IDefectList } from "../types/backend";
export const getDefectList=()=>{
    return axios.get<IBackendRes<IDefectList>>("/defect/list");
}


