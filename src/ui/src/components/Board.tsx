import React, { useState, useEffect } from 'react';
import { RouteComponentProps, Redirect } from 'react-router-dom';
import { BoardDto, BoardsClient, TopicShortDto } from '../api/ImageboardClient';
import { Link as RouterLink } from 'react-router-dom';
import { Skeleton, SpeedDial, SpeedDialIcon } from '@material-ui/lab';
import { Typography, Link, Box, Breadcrumbs, useTheme, Tooltip } from '@material-ui/core';
import MaterialTable, { Column } from 'material-table';
import { ShortText, PersonOutline, ChatBubbleOutline, Dashboard } from '@material-ui/icons';
import { AddTopicDialog } from './AddTopicDialog';
import { ErrorAlert } from './ErrorAlert';

type BoardParams = {
    shortUrl: string
}

export interface BoardLinkState {
    title?: string,
    description?: string,
}

interface BoardProps extends RouteComponentProps<BoardParams, any, BoardLinkState> {

}

export const Board: React.FunctionComponent<BoardProps> = (props) => {
    const theme = useTheme();

    const [data, setData] = useState<BoardDto | null>(null);

    const [title, setTitle] = useState(props.location.state?.title);
    const [description, setDescription] = useState(props.location.state?.description);

    const [addTopicFormIsVisible, setAddTopicFormIsVisible] = useState(false);

    const [newTopicId, setNewTopicId] = useState<number | undefined>(undefined);

    const [hasError, setHasError] = useState(false);

    useEffect(() => {
        const client = new BoardsClient(process.env.REACT_APP_API_URL);

        client.get(props.match.params.shortUrl)
            .then((data: BoardDto) => {
                setTitle(data.title);
                setDescription(data.description);
                setData(data);
            })
            .catch((error) => {
                setHasError(true);
            });
    }, [props.match.params.shortUrl]);

    useEffect(() => {
        document.title = props.match.params.shortUrl + " - Imageboard"
    }, [props.match.params.shortUrl]);

    if (hasError) {
        return (
            <Box mt={3}>
                <ErrorAlert />
            </Box>
        )
    }

    let content;

    if (data == null) {
        content = <Skeleton variant="text" height={150} />;
    }
    else {
        content = (
            <MaterialTable
                options={{ showTitle: false, draggable: false, pageSize: 5, pageSizeOptions: [5, 10, 20, 50, 100] }}
                localization={{ body: { emptyDataSourceMessage: "No topics at this board. Be the first, create one!" } }}
                columns={[
                    {
                        ...(true && ({ width: "80%" } as object)),
                        title: <Box style={{ display: 'flex', alignItems: 'center' }}><ShortText style={{ marginRight: 5 }} /> Topic</Box>,
                        field: "post.text",
                        render: (rowData: TopicShortDto) =>
                            <Box>
                                <Link component={RouterLink} to={{ pathname: "/topic/" + rowData.id, state: { title: rowData.title, boardShortUrl: rowData.board?.shortUrl } }} >
                                    <Typography variant="h6">{rowData.title}</Typography>
                                </Link>

                                <Typography  style={{ marginBottom: theme.spacing(1) }} variant="body2">{(rowData.text?.length ?? 0) > 120 ? rowData.text?.substr(0, 120) + "..." : rowData.text}</Typography>

                                <Box style={{ display: 'flex', alignItems: 'center'}}>

                                    <Typography variant="body2" style={{marginRight: theme.spacing(1), display: 'flex', alignItems: 'center'}}>
                                        <PersonOutline style={{ marginRight: 5 }} />
                                        {rowData.signature ?? "Anonymous"}
                                    </Typography>

                                    <Typography variant="body2" style={{marginRight: theme.spacing(1), display: 'flex', alignItems: 'center'}}>
                                        <ChatBubbleOutline style={{ marginRight: 5 }} />
                                        {rowData.postCount}
                                    </Typography>
                                </Box>
                            </Box>,
                        customFilterAndSearch: (filter: any, rowData: TopicShortDto, columnDef: Column<TopicShortDto>) =>
                            (rowData.title?.toLowerCase().includes(filter.toLowerCase()) || rowData.text?.toLowerCase().includes(filter.toLowerCase())) ?? false
                    },
                    {
                        title: "Last updated",
                        field: "lastUpdated",
                        render: (rowData: TopicShortDto) => <Typography title={rowData.lastUpdated?.local().format("LLL")} variant="body2">{rowData.lastUpdated?.fromNow()}</Typography>
                    }
                ]}
                data={data.topics ?? []}
            />
        )
    }

    if (newTopicId != null) {
        return <Redirect to={{ pathname: "/topic/" + newTopicId, state: { created: true } }} />
    }

    return (
        <Box>
            <Box mb={3} mt={2}>
                <Breadcrumbs aria-label="breadcrumb">
                    <Link color="inherit" style={{ display: 'flex' }} component={RouterLink} to="/" >
                        <Dashboard style={{ marginRight: 5, width: 20, height: 20 }} />
                        Boards
                    </Link>
                    <Typography color="textPrimary" aria-current="page">{props.match.params.shortUrl}</Typography>
                </Breadcrumbs>
            </Box>

            <Box mb={2}>
                {title == null ? <Skeleton variant="text" /> : <Typography variant="h4">{title}</Typography>}
            </Box>

            <Box mb={2}>
                {description == null ? <Skeleton variant="text" /> : <Typography variant="subtitle1">{description}</Typography>}
            </Box>

            {content}


            <Tooltip title="Create topic" >
                <SpeedDial
                    style={{ position: "fixed", bottom: theme.spacing(3), right: theme.spacing(3) }}
                    ariaLabel="addPost"
                    open={false}
                    icon={<SpeedDialIcon />}
                    onClick={() => setAddTopicFormIsVisible(true)}
                />
            </Tooltip>

            {addTopicFormIsVisible ?
                <AddTopicDialog
                    boardId={data?.id ?? 0}
                    open={true}
                    changeVisibilityHandler={setAddTopicFormIsVisible}
                    boardTitle={title ?? ""}
                    onSubmitted={(topicId) => setNewTopicId(topicId)}
                /> :
                ""
            }
        </Box>
    )
}