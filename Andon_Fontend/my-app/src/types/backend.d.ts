export interface IBackendRes<T>{
    message:string,
    statusCode:number|string,
    data:T
}
export interface IDefectList{
    id:number
    lineCode:string,
    routerName:string,
    errorName:string,
    routeCode:string,
    routeName:string,
    errorDescription:string,
    defectedBy:string,
    operator:string,
    reason:string,
    countermeasure:string,
    repairer:string,
    beginOccur:Date,
    beginFix:Date,
    finishFix:Date,
    repairDuration:number,
    status:number,
    createdAt:Date
} 
export interface DefectState{
    success:Boolean,
    data:DefectState[],
    message:string
}