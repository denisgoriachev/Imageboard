import React, { ComponentType, ReactElement } from 'react';
import { RouteComponentProps, RouteProps, Route } from 'react-router-dom';

type PropsRouteProps<P> = P extends RouteComponentProps<any>
  ? RouteProps & {
      component: ComponentType<P>
      withProps?: Omit<P, keyof RouteComponentProps<any>>
    }
  : never
  
export const PropsRoute = <P extends RouteComponentProps<any>>({
  component: WrappedComponent,
  withProps,
  ...routeProps
}: PropsRouteProps<P>): ReactElement<P> => {
  return (
    <Route
      {...routeProps}
      render={childProps => {
        return <WrappedComponent {...childProps} {...withProps} />
      }}
    />
  )
}