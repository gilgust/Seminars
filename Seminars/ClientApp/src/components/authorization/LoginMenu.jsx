import React, { Component, Fragment } from 'react';
import { NavItem, NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';
import { ApplicationPaths } from './ApiAuthorizationConstants';

export class LoginMenu extends Component {
    constructor(props) {
        super(props);

        this.state = {
            isAuthenticated: false,
            userName: null
        };
    }

    render() {
        const { isAuthenticated, userName } = this.state;

        if (!isAuthenticated) {
            const registerPath = `${ApplicationPaths.Register}`;
            const loginPath = `${ApplicationPaths.Login}`;
            return this.anonymousView(registerPath, loginPath);
        } else {
            const profilePath = `${ApplicationPaths.Profile}`;
            const logoutPath = { pathname: `${ApplicationPaths.LogOut}`, state: { local: true } };
            return this.authenticatedView(userName, profilePath, logoutPath);
        }
    }

    authenticatedView(userName, profilePath, logoutPath) {
        return (<Fragment>
                    <NavItem>
                        <NavLink tag={Link} className="text-dark" to={profilePath}>Hello {userName}</NavLink>
                    </NavItem>
                    <NavItem>
                        <NavLink tag={Link} className="text-dark" to={logoutPath}>Logout</NavLink>
                    </NavItem>
                </Fragment>);

    }

    anonymousView(registerPath, loginPath) {
        return (<Fragment>
                    <NavItem>
                        <NavLink tag={Link} className="text-dark" to={registerPath}>Register</NavLink>
                    </NavItem>
                    <NavItem>
                        <NavLink tag={Link} className="text-dark" to={loginPath}>Login</NavLink>
                    </NavItem>
                </Fragment>);
    }
}