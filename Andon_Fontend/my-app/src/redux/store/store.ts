import {type Action,configureStore,type ThunkAction} from '@reduxjs/toolkit'
import defectReducer from '../slice/defectSlice';
export const store = configureStore({
  reducer: {
     defect:defectReducer
  },
});
export type AppDispatch = typeof store.dispatch;
export type RootState = ReturnType<typeof store.getState>;
export type AppThunk<ReturnType = void> = ThunkAction<
  ReturnType,
  RootState,
  unknown,
  Action<string>
>;
