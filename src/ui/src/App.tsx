import React from 'react';
import { Container, ThemeProvider, createMuiTheme, makeStyles, createStyles, Theme, useScrollTrigger, Zoom, Fab, CssBaseline, Link } from '@material-ui/core';
import {
  BrowserRouter as Router,
  Switch,
  Route,
  Link as RouterLink
} from 'react-router-dom';
import { Home } from './components/Home';
import { Board } from './components/Board';
import { Topic } from './components/Topic';
import { KeyboardArrowUp } from '@material-ui/icons';
import { ErrorAlert } from './components/ErrorAlert';

import "./styles.scss"

const theme = createMuiTheme({
  spacing: 8
});

interface Props {
  /**
   * Injected by the documentation to work in an iframe.
   * You won't need it on your project.
   */
  window?: () => Window;
  children: React.ReactElement;
}

const useStyles = makeStyles((theme: Theme) =>
  createStyles({
    root: {
      position: 'fixed',
      bottom: theme.spacing(2),
      left: theme.spacing(2),
    }
  }),
);

function ScrollTop(props: Props) {
  const { children, window } = props;
  const classes = useStyles();
  // Note that you normally won't need to set the window ref as useScrollTrigger
  // will default to window.
  // This is only being set here because the demo is in an iframe.
  const trigger = useScrollTrigger({
    target: window ? window() : undefined,
    disableHysteresis: true,
    threshold: 100,
  });

  const handleClick = (event: React.MouseEvent<HTMLDivElement>) => {
    const anchor = ((event.target as HTMLDivElement).ownerDocument || document).querySelector(
      '#back-to-top-anchor',
    );

    if (anchor) {
      anchor.scrollIntoView({ behavior: 'auto', block: 'center' });
    }
  };

  return (
    <Zoom in={trigger}>
      <div onClick={handleClick} role="presentation" className={classes.root}>
        {children}
      </div>
    </Zoom>
  );
}

function App() {
  return (
    <Router>
      <ThemeProvider theme={theme}>
        <CssBaseline />
        <div id="back-to-top-anchor"></div>
        <Container>
          <Switch>
            <Route exact path="/:shortUrl" component={Board} />
            <Route exact path="/topic/:id" component={Topic} />
            <Route exact path="/" component={Home} />
            <Route default>
              <ErrorAlert statusCode={404}>
                Page not found! Please, return to <Link color="inherit" underline="always" component={RouterLink} to="/" >home page</Link>
              </ErrorAlert>
            </Route>
          </Switch>
        </Container>
        <ScrollTop>
          <Fab color="secondary" size="small" aria-label="scroll back to top">
            <KeyboardArrowUp />
          </Fab>
        </ScrollTop>
      </ThemeProvider>
    </Router>
  );
}

export default App;
